using FluentValidation;
using Seemus.Domain.Dtos.User;

namespace Seemus.Api.Validators.Artist
{
    public class RegisterArtistValidator : AbstractValidator<RegisterArtistDto>
    {
        public RegisterArtistValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(100).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MaximumLength(100).NotEmpty();
        }
    }
}