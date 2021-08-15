using System.Collections.Generic;

namespace MovieFinder.Business.Dtos
{
    public class ImdbMovieResult
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string FullTitle { get; set; }

        public string Type { get; set; }

        public string Year { get; set; }

        public string Plot { get; set; }

        public string Genres { get; set; }

        public string Countries { get; set; }

        public string Languages { get; set; }

        public string Tagline { get; set; }

        public List<string> KeywordList { get; set; }

        public string ErrorMessage { get; set; }
    }
}
