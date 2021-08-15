﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using MovieFinder.Business.Dtos;
using MovieFinder.Business.Models;
using MovieFinder.Business.Services.Interfaces;
using MovieFinder.Common.Enums;
using MovieFinder.Dal;
using MovieFinder.Dal.Models;
using Newtonsoft.Json;

namespace MovieFinder.Business.Services
{
    public class MovieService : IMovieService
    {
        private const string BaseImdbUrl = @"https://imdb-api.com/en/API";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _imdbApiKey;
        private readonly string _youtubeApplicationName;
        private readonly string _youtubeApiKey;
        private readonly MovieFinderDbContext _context;

        public MovieService
        (
            IHttpClientFactory httpClientFactory,
            IOptions<ApiKeys> apiKeysOptions,
            MovieFinderDbContext context
        )
        {
            _httpClientFactory = httpClientFactory;
            _imdbApiKey = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Imdb")?.Secret;
            _youtubeApplicationName = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Youtube")?.ApplicationName;
            _youtubeApiKey = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Youtube")?.Secret;
            _context = context;
        }

        public async Task<ImdbTitleResults> GetListOfImdbTitles(string movieTitle, int? year = null)
        {
            string requestUrl = BuildImdbRequestUrl("SearchTitle", movieTitle);

            if (year.HasValue)
            {
                requestUrl += ($" {year}");
            }
            
            var result = await GetResponseFromImdb<ImdbTitleResults>(requestUrl);

            return result;
        }

        public async Task<AggregatedMovieResult> GetImdbMovie(string imdbId)
        {
            // TODO: check the cache first - if any don't save/read to DB or go to IMDB.
            // TODO: check the database second - if any don't go to IMDB.
            // TODO: check the IMDB last - if any give an option to update the cache and the database.

            // string requestUrl = BuildImdbRequestUrl("Title", imdbId);

            // var result = await GetResponseFromImdb<AggregatedMovieResult>(requestUrl);
            
            var result = new AggregatedMovieResult
            {
                Id = "tt1375666",
                Title = "Inception",
                FullTitle = "Inception (2010)",
                Type = "Movie",
                Year = "2010",
                Plot = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb&#39;s rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible, inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea, but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming.\"",
                Genres = "Action, Adventure, Sci-Fi",
                Countries = "USA, UK",
                ErrorMessage = ""
            };

            var youtubeTrailers = await GetYoutubeTrailers($"{result.Title} {result.Year}");

            result.YoutubeTrailers = youtubeTrailers;

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new Exception("Unable to find any results on IMDB");
            }

            await SaveMovieAsync(result);

            return result;
        }

        private string BuildImdbRequestUrl(string action, string searchValue)
        {
            if (string.IsNullOrEmpty(_imdbApiKey))
            {
                throw new Exception("IMDB API key is missing.");
            }
            
            return $"{BaseImdbUrl}/{action}/{_imdbApiKey}/{searchValue}";
        }

        private async Task<TResult> GetResponseFromImdb<TResult>(string requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResult>(result);
            }
            
            throw new Exception("Unable to get the response from IMDB API.");
        }

        private async Task<int> SaveMovieAsync(AggregatedMovieResult aggregatedMovieResult)
        {
            // TODO: add additional fields to the database.
            var movie = new Movie
            {
                Name = aggregatedMovieResult.FullTitle,
                ImdbData = new ImdbData
                {
                    ImdbId = aggregatedMovieResult.Id,
                    MovieName = aggregatedMovieResult.FullTitle,
                    ReleaseYear = int.TryParse(aggregatedMovieResult.Year, out var year) ? year : 0
                },
                VideoData = new List<VideoData>()
            };

            foreach (var youtubeTrailer in aggregatedMovieResult.YoutubeTrailers.YoutubeResults)
            {
                movie.VideoData.Add(new VideoData
                {
                    VideoSourceEnum = VideoSourceEnum.YouTube,
                    VideoUrl = $"https://www.youtube.com/watch?v={youtubeTrailer.Id}",
                    ThumbnailUrl = youtubeTrailer.ThumbnailUrl,
                    Name = youtubeTrailer.Name
                });
            }

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            
            return movie.Id;
        }

        private async Task<YoutubeTrailers> GetYoutubeTrailers(string movieName)
        {
            var youTubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = _youtubeApplicationName,
                ApiKey = _youtubeApiKey
            });

            var searchRequest = youTubeService.Search.List("snippet");
            searchRequest.Q = movieName;
            searchRequest.MaxResults = 10;

            var searchResponse = await searchRequest.ExecuteAsync();

            var trailerResults = new List<YoutubeResult>();
            if (searchResponse.Items != null)
            {
                foreach (var item in searchResponse.Items)
                {
                    if (item.Id.Kind == "youtube#video")
                    {
                        trailerResults.Add(new YoutubeResult
                        {
                            Id = item.Id.VideoId,
                            Name = item.Snippet.Title,
                            ThumbnailUrl = item.Snippet.Thumbnails.High.Url
                        });
                    }
                }
            }

            return new YoutubeTrailers
            {
                YoutubeResults = trailerResults
            };
        }
    }
}
