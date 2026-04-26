using FluentValidation;
using JobLink.Domain.Common.Constants;
using JobLink.Application.Common.Validators;

namespace JobLink.Application.Features.Companies.Profile.Commands.RegisterCompany;

public sealed class RegisterCompanyCommandValidator : AbstractValidator<RegisterCompanyCommand>
{
    public RegisterCompanyCommandValidator()
    {
        RuleFor(x => x.User)
            .SetValidator(new RegisterUserValidator());

        RuleFor(x => x.Name)
            .MaximumLength(CompanyProfileConstraints.NameMaxLength)
            .MinimumLength(CompanyProfileConstraints.NameMinLength)
            .NotEmpty();

        RuleFor(x => x.Industry)
            .MaximumLength(CompanyProfileConstraints.IndustryMaxLength)
            .MinimumLength(CompanyProfileConstraints.IndustryMinLength)
            .NotEmpty();
    }
}