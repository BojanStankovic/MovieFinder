using System;
using MovieFinder.Common.Constants;

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
            
            return $"{UrlConstants.ImdbBaseUrl}/{action}/{imdbApiKey}/{searchValue}";
        }

        public static string BuildYoutubeWatchVideoUrl(string youtubeVideoId)
        {
            return $"{UrlConstants.YoutubeWatchVideoUrlPrefix}{youtubeVideoId}";
        }
    }
}
