using FluentValidation;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.UpdateExperience;

public sealed class UpdateExperienceCommandValidator : AbstractValidator<UpdateExperienceCommand>
{
    public UpdateExperienceCommandValidator()
    {
        RuleFor(x => x.Company)
            .MaximumLength(ExperienceConstraints.CompanyNameMaxLength)
            .MinimumLength(ExperienceConstraints.CompanyNameMinLength)
            .When(x => x.Company != null);

        RuleFor(x => x.Position)
            .MaximumLength(ExperienceConstraints.PositionMaxLength)
            .MinimumLength(ExperienceConstraints.PositionMinLength)
            .When(x => x.Position != null);

        RuleFor(x => x.Country)
            .MaximumLength(ExperienceConstraints.CountryMaxLength)
            .MinimumLength(ExperienceConstraints.CountryMinLength)
            .When(x => x.Country != null);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue && x.StartDate.HasValue);

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Salary.HasValue);

        RuleFor(x => x.Description)
            .MaximumLength(ExperienceConstraints.DescriptionMaxLength)
            .MinimumLength(ExperienceConstraints.DescriptionMinLength)
            .When(x => x.Description != null);
    }
}
