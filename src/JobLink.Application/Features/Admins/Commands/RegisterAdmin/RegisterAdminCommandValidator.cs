using FluentValidation;
using JobLink.Application.Common.Validators;

namespace JobLink.Application.Features.Admins.Commands.RegisterAdmin;

public sealed class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
{
    public RegisterAdminCommandValidator()
    {
        RuleFor(x => x.User)
            .SetValidator(new RegisterUserValidator());

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender");
    }
}