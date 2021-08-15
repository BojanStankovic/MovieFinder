using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MovieFinder.Business.Dtos;
using MovieFinder.Business.Models;
using MovieFinder.Business.Services.Interfaces;
using Newtonsoft.Json;

namespace MovieFinder.Business.Services
{
    public class MovieService : IMovieService
    {
        private const string BaseImdbUrl = @"https://imdb-api.com/en/API";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _imdbApiKey;

        public MovieService
        (
            IHttpClientFactory httpClientFactory,
            IOptions<ApiKeys> apiKeysOptions
        )
        {
            _httpClientFactory = httpClientFactory;
            _imdbApiKey = apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Imdb")?.Secret;
        }

        public async Task<ImdbTitleResults> GetListOfImdbTitles(string movieTitle, int? year = null)
        {
            string requestUrl = BuildRequestUrl("SearchTitle", movieTitle);

            if (year.HasValue)
            {
                requestUrl += ($" {year}");
            }
            
            var result = await GetResponseFromImdb<ImdbTitleResults>(requestUrl);

            return result;
        }

        public async Task<ImdbMovieResult> GetImdbMovie(string imdbId)
        {
            string requestUrl = BuildRequestUrl("Title", imdbId);

            var result = await GetResponseFromImdb<ImdbMovieResult>(requestUrl);

            return result;
        }

        private string BuildRequestUrl(string action, string searchValue)
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
    }
}
