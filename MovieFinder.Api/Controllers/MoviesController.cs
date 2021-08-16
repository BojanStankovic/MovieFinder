using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieFinder.Api.Models;
using MovieFinder.Business.Dtos;
using MovieFinder.Business.Services.Interfaces;

namespace MovieFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/<MoviesController>/title
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetMovieListRequestModel requestModel)
        {
            var requestDto = new GetMovieListRequestDto
            {
                MovieTitle = requestModel.MovieTitle,
                MovieReleaseYear = requestModel.MovieReleaseYear
            };

            var result = await _movieService.GetListOfImdbTitles(requestDto);
            return Ok(result);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> Get(string imdbId)
        {
            var result = await _movieService.GetImdbMovie(imdbId);
            return Ok(result);
        }
    }
}
