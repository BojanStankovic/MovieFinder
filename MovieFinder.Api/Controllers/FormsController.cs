using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieFinder.Api.Models;
using MovieFinder.Business.Dtos;
using MovieFinder.Business.Services.Interfaces;

namespace MovieFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormsController(IFormService formService)
        {
            _formService = formService;
        }

        // POST: api/<FormsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] RequestTrailerRequestModel requestModel)
        {
            var requestDto = new RequestTrailerRequestDto
            {
                ImdbId = requestModel.ImdbId,
                EmailAddress = requestModel.EmailAddress,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };

            var entityId = await _formService.RequestTrailer(requestDto);

            return CreatedAtAction(nameof(Post), new
            {
                Id = entityId
            });
        }
    }
}
