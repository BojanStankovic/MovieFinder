using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MovieFinder.Business.Dtos;
using MovieFinder.Business.Helpers;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _imdbApiKey;
        private readonly string _youtubeApplicationName;
        private readonly string _youtubeApiKey;
        private readonly MovieFinderDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly int _cacheAbsoluteExpirationRelativeToNow;
        private readonly CacheItemPriority _cachePriority;

        public MovieService
        (
            IHttpClientFactory httpClientFactory,
            IOptions<ApiKeys> apiKeysOptions,
            IOptions<CacheSettings> cacheSettings,
            MovieFinderDbContext context,
            IMemoryCache cache
        )
        {
            _httpClientFactory = httpClientFactory;
            _imdbApiKey = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Imdb")?.Secret;
            _youtubeApplicationName = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Youtube")?.ApplicationName;
            _youtubeApiKey = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Youtube")?.Secret;
            _context = context;
            _cache = cache;
            _cacheAbsoluteExpirationRelativeToNow = cacheSettings?.Value?.AbsoluteExpirationRelativeToNowInSeconds ?? 0;
            _cachePriority = cacheSettings?.Value?.Priority ?? CacheItemPriority.Normal;
        }

        public async Task<ImdbTitleResults> GetListOfImdbTitles(GetMovieListRequestDto requestModel)
        {
            string requestUrl = UrlBuilders.BuildImdbRequestUrl("SearchTitle", requestModel.MovieTitle, _imdbApiKey);

            if (requestModel.MovieReleaseYear.HasValue)
            {
                requestUrl += ($" {requestModel.MovieReleaseYear}");
            }
            
            var result = await GetResponseFromImdb<ImdbTitleResults>(requestUrl);

            return result;
        }

        public async Task<AggregatedMovieResult> GetImdbMovie(string imdbId)
        {
            if (_cache.TryGetValue(imdbId, out AggregatedMovieResult result))
            {
                return result;
            }

            result = await GetSavedResult(imdbId);

            if (result is null)
            {
                result = await GetNewResult(imdbId);
            }
            else
            {
                CacheResult(result);
                return result;
            }

            await SaveResultAsync(result);
            CacheResult(result);

            return result;
        }

        private async Task<AggregatedMovieResult> GetSavedResult(string imdbId)
        {
            var movieResult = await _context.Movies
                .Where(d => d.ImdbDataId == imdbId)
                .Include(d => d.ImdbData)
                .Include(m => m.VideoData)
                .FirstOrDefaultAsync();

            if (movieResult is null)
            {
                return null;
            }

            var youtubeResults = new List<YoutubeResult>();
            foreach (var videoData in movieResult.VideoData)
            {
                if (videoData.VideoSourceEnum != VideoSourceEnum.YouTube)
                {
                    continue;
                }

                youtubeResults.Add(new YoutubeResult
                {
                    VideoUrl = videoData.VideoUrl,
                    Name = videoData.Name,
                    ThumbnailUrl = videoData.ThumbnailUrl
                });
            }

            return new AggregatedMovieResult
            {
                Id = movieResult.ImdbDataId,
                Title = movieResult.ImdbData.MovieName,
                FullTitle = movieResult.ImdbData.FullTitle,
                Year = movieResult.ImdbData.ReleaseYear.ToString(),
                YoutubeTrailers = new YoutubeTrailers
                {
                    YoutubeResults = youtubeResults
                }
            };
        }

        private async Task<AggregatedMovieResult> GetNewResult(string imdbId)
        {
            string requestUrl = UrlBuilders.BuildImdbRequestUrl("Title", imdbId, _imdbApiKey);
            var result = await GetResponseFromImdb<AggregatedMovieResult>(requestUrl);

            var youtubeTrailers = await GetYoutubeTrailers($"{result.Title} {result.Year} trailer");

            result.YoutubeTrailers = youtubeTrailers;

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new Exception("Unable to find any results on IMDB");
            }

            return result;
        }

        private async Task<TResult> GetResponseFromImdb<TResult>(string requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Add("Accept", "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResult>(result);
            }
            
            throw new Exception("Unable to get the response from IMDB API.");
        }

        private async Task<YoutubeTrailers> GetYoutubeTrailers(string searchString)
        {
            var youTubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = _youtubeApplicationName,
                ApiKey = _youtubeApiKey
            });

            var searchRequest = youTubeService.Search.List("snippet");
            searchRequest.Q = searchString;
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
                            VideoUrl = UrlBuilders.BuildYoutubeWatchVideoUrl(item.Id.VideoId),
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

        private void CacheResult(AggregatedMovieResult result)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheAbsoluteExpirationRelativeToNow),
                Priority = _cachePriority,
                Size = 1
            };

            _cache.Set(result.Id, result, options);
        }

        private async Task SaveResultAsync(AggregatedMovieResult aggregatedMovieResult)
        {
            var movie = new Movie
            {
                Name = aggregatedMovieResult.Title,
                ImdbData = new ImdbData
                {
                    ImdbId = aggregatedMovieResult.Id,
                    MovieName = aggregatedMovieResult.Title,
                    FullTitle = aggregatedMovieResult.FullTitle,
                    ReleaseYear = int.TryParse(aggregatedMovieResult.Year, out var year) ? year : 0
                },
                VideoData = new List<VideoData>(),
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            foreach (var youtubeTrailer in aggregatedMovieResult.YoutubeTrailers.YoutubeResults)
            {
                movie.VideoData.Add(new VideoData
                {
                    VideoSourceEnum = VideoSourceEnum.YouTube,
                    VideoUrl = youtubeTrailer.VideoUrl,
                    ThumbnailUrl = youtubeTrailer.ThumbnailUrl,
                    Name = youtubeTrailer.Name
                });
            }

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
    }
}
