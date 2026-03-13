using FluentValidation;
using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Common.Validators;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address")
            .MaximumLength(UserConstraints.EmailMaxLength);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(UserConstraints.PasswordHashMaxLength)
            .MinimumLength(UserConstraints.PasswordHashMinLength);
    }
}