namespace MovieFinder.Business.Dtos
{
    public class GetMovieListRequestDto
    {
        public string MovieTitle { get; set; }

        public int? MovieReleaseYear { get; set; } = null;
    }
}
