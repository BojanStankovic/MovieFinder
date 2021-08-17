using System;

namespace MovieFinder.Dal.Models
{
    public class TrailerRequestDetails
    {
        public int Id { get; set; }

        public string RequestedMovieTrailerImdbId { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Created { get; set; }

        public bool IsFirstRequest { get; set; } = false;
    }
}
