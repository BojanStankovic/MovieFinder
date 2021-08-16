using System.Collections.Generic;

namespace MovieFinder.Business.Dtos
{
    public class YoutubeTrailers
    {
        public List<YoutubeResult> YoutubeResults { get; set; }
    }

    public class YoutubeResult
    {
        public string VideoUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Name { get; set; }
    }
}
