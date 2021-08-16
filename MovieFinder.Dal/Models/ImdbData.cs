namespace MovieFinder.Dal.Models
{
    public class ImdbData
    {
        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string MovieName { get; set; }

        public int ReleaseYear { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
    }
}
