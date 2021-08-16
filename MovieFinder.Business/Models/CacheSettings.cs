using Microsoft.Extensions.Caching.Memory;

namespace MovieFinder.Business.Models
{
    public class CacheSettings
    {
        public int AbsoluteExpirationRelativeToNowInSeconds { get; set; }

        public int SizeLimit { get; set; }

        public double CompactionPercentage { get; set; }

        public CacheItemPriority Priority { get; set; }
    }
}
