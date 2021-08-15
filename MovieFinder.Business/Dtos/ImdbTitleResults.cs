using System.Collections.Generic;

namespace MovieFinder.Business.Dtos
{
    public class ImdbTitleResults
    {
        public string SearchType { get; set; }

        public string Expression { get; set; }

        public List<ImdbResult> Results { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class ImdbResult
    {
        public string Id { get; set; }

        public string ResultType { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
