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
            .NotEmpty();

        RuleFor(x => x.Position)
            .MaximumLength(ExperienceConstraints.PositionMaxLength)
            .MinimumLength(ExperienceConstraints.PositionMinLength)
            .NotEmpty();

        RuleFor(x => x.Country)
            .MaximumLength(ExperienceConstraints.CountryMaxLength)
            .MinimumLength(ExperienceConstraints.CountryMinLength)
            .NotEmpty();

        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThan(x => x.StartDate);

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Description)
            .MaximumLength(ExperienceConstraints.DescriptionMaxLength)
            .MinimumLength(ExperienceConstraints.DescriptionMinLength)
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
