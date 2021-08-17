using System.Threading.Tasks;
using MovieFinder.Business.Dtos;

namespace MovieFinder.Business.Services.Interfaces
{
    public interface IFormService
    {
        Task<int> RequestTrailer(RequestTrailerRequestDto requestModel);
    }
}
