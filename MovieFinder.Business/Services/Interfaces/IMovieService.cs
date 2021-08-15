using System.Threading.Tasks;
using MovieFinder.Business.Dtos;

namespace MovieFinder.Business.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ImdbTitleResults> GetListOfImdbTitles(string movieTitle, int? year);
    }
}
