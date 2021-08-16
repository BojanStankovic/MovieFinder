using System;
using MovieFinder.Common;

namespace MovieFinder.Business.Helpers
{
    public static class UrlBuilders
    {
        public static string BuildImdbRequestUrl(string action, string searchValue, string imdbApiKey)
        {
            if (string.IsNullOrEmpty(imdbApiKey))
            {
                throw new Exception("IMDB API key is missing.");
            }
            
            return $"{Constants.ImdbBaseUrl}/{action}/{imdbApiKey}/{searchValue}";
        }

        public static string BuildYoutubeWatchVideoUrl(string youtubeVideoId)
        {
            return $"{Constants.YoutubeWatchVideoUrlPrefix}{youtubeVideoId}";
        }
    }
}
