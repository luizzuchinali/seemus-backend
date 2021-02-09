using FluentValidation;
using Seemus.Domain.Dtos.Account;

namespace Seemus.Api.Validators.Account
{
    public class ChangeAccountPasswordValidator : AbstractValidator<ChangeAccountPasswordDto>
    {
        public ChangeAccountPasswordValidator()
        {
            RuleFor(x => x.CurrentPassword).MaximumLength(100).NotEmpty();
            RuleFor(x => x.NewPassword).MaximumLength(100).NotEmpty();
            RuleFor(x => x.RepeatNewPassword)
                .MaximumLength(100).NotEmpty()
                .Must((dto, value) => dto.NewPassword == dto.RepeatNewPassword)
                    .WithMessage("Deve ser igual à nova senha.");
        }
    }
}