using FluentValidation;
using JobLink.Application.Common.Validators;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.Companies.Jobs.Commands.UpdateJob;

public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .MinimumLength(JobConstraints.TitleMinLength)
            .MaximumLength(JobConstraints.TitleMaxLength)
            .When(x => x.Title != null);

        RuleFor(x => x.Description)
            .MinimumLength(JobConstraints.DescriptionMinLength)
            .MaximumLength(JobConstraints.DescriptionMaxLength)
            .When(x => x.Description != null);

        RuleFor(x => x.Requirements)
            .MinimumLength(JobConstraints.RequirementsMinLength)
            .MaximumLength(JobConstraints.RequirementsMaxLength)
            .When(x => x.Requirements != null);

        RuleFor(x => x.JobType)
            .IsInEnum()
            .When(x => x.JobType != null);

        RuleFor(x => x.LocationType)
            .IsInEnum()
            .When(x => x.LocationType != null);

        RuleFor(x => x.Country)
            .MaximumLength(AddressConstraints.CountryMaxLength)
            .MinimumLength(AddressConstraints.CountryMinLength)
            .When(x => x.Country != null);

        RuleFor(x => x.City)
            .MaximumLength(AddressConstraints.CityMaxLength)
            .MinimumLength(AddressConstraints.CityMinLength)
            .When(x => x.City != null);

        RuleFor(x => x.Area)
            .MaximumLength(AddressConstraints.AreaMaxLength)
            .MinimumLength(AddressConstraints.AreaMinLength)
            .When(x => x.Area != null);

        RuleFor(x => x.MinSalary)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinSalary != null);

        RuleFor(x => x.MaxSalary)
            .GreaterThan(x => x.MinSalary).WithMessage("Max salary must be greater than min salary")
            .When(x => x.MaxSalary != null && x.MinSalary != null);

        RuleFor(x => x.ExperienceLevel)
            .IsInEnum()
            .When(x => x.ExperienceLevel != null);

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
            .When(x => x.ExpirationDate != null);
    }
}