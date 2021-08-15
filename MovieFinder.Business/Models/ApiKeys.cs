using System.Collections.Generic;

namespace MovieFinder.Business.Models
{
    public class ApiKeys
    {
        public List<ExternalApi> ExternalApis { get; set; }
    }

    public class ExternalApi
    {
        public string Name { get; set; }

        public string Secret { get; set; }
    }
}
