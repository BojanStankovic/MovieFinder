using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieFinder.Business.Dtos;
using MovieFinder.Business.Services.Interfaces;
using MovieFinder.Dal;
using MovieFinder.Dal.Models;

namespace MovieFinder.Business.Services
{
    public class FormService : IFormService
    {
        private readonly MovieFinderDbContext _context;

        public FormService(MovieFinderDbContext context)
        {
            _context = context;
        }

        public async Task<int> RequestTrailer(RequestTrailerRequestDto requestModel)
        {
            var existingTrailerRequestDetailsEntity = await _context.TrailerRequestDetails
                .Where(trd => trd.RequestedMovieTrailerImdbId == requestModel.ImdbId)
                .FirstOrDefaultAsync();

            if (existingTrailerRequestDetailsEntity?.EmailAddress == requestModel.EmailAddress)
            {
                throw new Exception(
                    $"The user with an email {requestModel.EmailAddress} already requested the trailer for a movie with an ImdbId={requestModel.ImdbId}");
            }

            var newTrailerRequestDetailsEntity = new TrailerRequestDetails
            {
                RequestedMovieTrailerImdbId = requestModel.ImdbId,
                EmailAddress = requestModel.EmailAddress,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Created = DateTime.UtcNow
            };

            if (existingTrailerRequestDetailsEntity is null)
            {
                newTrailerRequestDetailsEntity.IsFirstRequest = true;
            }

            await _context.TrailerRequestDetails.AddAsync(newTrailerRequestDetailsEntity);
            await _context.SaveChangesAsync();

            return newTrailerRequestDetailsEntity.Id;
        }
    }
}
