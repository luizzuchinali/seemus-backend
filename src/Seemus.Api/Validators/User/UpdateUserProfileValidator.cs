using FluentValidation;
using Seemus.Domain.Dtos.User;

namespace Seemus.Api.Validators.User
{
    public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileDto>
    {
        public UpdateUserProfileValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        }
    }
}