using FluentValidation;
using Seemus.Domain.Dtos.Account;

namespace Seemus.Api.Validators.Account
{
    public class AuthValidator : AbstractValidator<AuthDto>
    {
        public AuthValidator()
        {
            RuleFor(X => X.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}