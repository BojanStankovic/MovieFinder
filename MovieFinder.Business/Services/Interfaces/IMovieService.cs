using System.Threading.Tasks;
using MovieFinder.Business.Dtos;

namespace MovieFinder.Business.Services.Interfaces
{
    public interface IMovieService
    {
        /// <summary>
        /// Gets the matching titles from IMDB based on the movie title and year
        /// </summary>
        /// <param name="movieTitle"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<ImdbTitleResults> GetListOfImdbTitles(string movieTitle, int? year);

        /// <summary>
        /// Gets an exact movie from the IMDB based on the IMDB entry ID
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<ImdbMovieResult> GetImdbMovie(string imdbId);
    }
}
