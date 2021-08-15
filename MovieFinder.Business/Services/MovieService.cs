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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<ApiKeys> _apiKeysOptions;

        public MovieService
        (
            IHttpClientFactory httpClientFactory,
            IOptions<ApiKeys> apiKeysOptions
        )
        {
            _httpClientFactory = httpClientFactory;
            _apiKeysOptions = apiKeysOptions;
        }

        public async Task<ImdbTitleResults> GetListOfImdbTitles(string movieTitle, int? year = null)
        {
            string baseImdbUrl = @"https://imdb-api.com/en/API";
            string imdbAction = @"SearchTitle";
            string imdbApiKey = _apiKeysOptions?.Value?.ExternalApis?.FirstOrDefault(ea => ea.Name == "Imdb")?.Secret;
            string requestUrl = $"{baseImdbUrl}/{imdbAction}/{imdbApiKey}/{movieTitle}";

            if (year.HasValue)
            {
                requestUrl = requestUrl + ($" {year}");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ImdbTitleResults>(result);
            }
            
            throw new Exception("Unable to get the response from IMDB API.");
        }
    }
}
