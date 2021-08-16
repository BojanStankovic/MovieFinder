using MovieFinder.Common.Enums;

namespace MovieFinder.Dal.Models
{
    public class VideoData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public VideoSourceEnum VideoSourceEnum { get; set; }

        public int MovieId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
    }
}
