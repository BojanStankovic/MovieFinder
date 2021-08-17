using FluentValidation;
using MovieFinder.Api.Models;

namespace MovieFinder.Api.Validators
{
    public class RequestTrailerRequestModelValidator : AbstractValidator<RequestTrailerRequestModel>
    {
        public RequestTrailerRequestModelValidator()
        {
            RuleFor(r => r.ImdbId)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.EmailAddress)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(r => r.FirstName)
                .NotNull()
                .NotEmpty();
        }
    }
}
