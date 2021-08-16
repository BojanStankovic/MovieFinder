namespace MovieFinder.Api.Models
{
    public class GetMovieListRequestModel
    {
        public string MovieTitle { get; set; }

        public int? MovieReleaseYear { get; set; } = null;
    }
}
