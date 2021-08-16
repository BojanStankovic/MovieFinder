using FluentValidation;
using MovieFinder.Api.Models;

namespace MovieFinder.Api.Validators
{
    public class GetMovieListRequestModelValidator : AbstractValidator<GetMovieListRequestModel>
    {
        public GetMovieListRequestModelValidator()
        {
            RuleFor(r => r.MovieTitle)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.MovieReleaseYear)
                .ExclusiveBetween(1800, 2800)
                .When(r => r.MovieReleaseYear != null);
        }
    }
}
