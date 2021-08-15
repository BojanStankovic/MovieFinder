using System.Collections.Generic;

namespace MovieFinder.Dal.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ImdbDataId { get; set; }

        // Navigation properties
        public ImdbData ImdbData { get; set; }

        public ICollection<VideoData> VideoData { get; set; }
    }
}
