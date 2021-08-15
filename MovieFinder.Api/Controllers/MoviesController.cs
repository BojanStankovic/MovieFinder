using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get([FromQuery] string title, [FromQuery] int? year = null)
        {
            var result = await _movieService.GetListOfImdbTitles(title, year);
            return Ok(result);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> Get(string imdbId)
        {
            var result = await _movieService.GetImdbMovie(imdbId);
            return Ok(result);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
