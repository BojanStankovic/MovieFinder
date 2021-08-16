namespace MovieFinder.Business.Dtos
{
    public class AggregatedMovieResult
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string FullTitle { get; set; }

        public string Year { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public YoutubeTrailers YoutubeTrailers { get; set; }
    }
}
