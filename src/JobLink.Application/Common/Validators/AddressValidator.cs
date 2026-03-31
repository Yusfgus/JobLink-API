using FluentValidation;
using JobLink.Domain.Common.Constants;
using JobLink.Domain.Common.ValueObjects;

namespace JobLink.Application.Common.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Country)
            .MinimumLength(AddressConstraints.CountryMinLength)
            .MaximumLength(AddressConstraints.CountryMaxLength)
            .When(x => !string.IsNullOrEmpty(x.Country));

        RuleFor(x => x.City)
            .MinimumLength(AddressConstraints.CityMinLength)
            .MaximumLength(AddressConstraints.CityMaxLength)
            .When(x => !string.IsNullOrEmpty(x.City));

        RuleFor(x => x.Area)
            .MinimumLength(AddressConstraints.AreaMinLength)
            .MaximumLength(AddressConstraints.AreaMaxLength)
            .When(x => !string.IsNullOrEmpty(x.Area));
    }
}
