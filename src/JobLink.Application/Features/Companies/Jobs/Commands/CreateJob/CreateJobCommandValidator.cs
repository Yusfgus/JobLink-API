using FluentValidation;
using JobLink.Application.Common.Validators;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.Companies.Jobs.Commands.CreateJob;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(JobConstraints.TitleMaxLength)
            .MinimumLength(JobConstraints.TitleMinLength)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(JobConstraints.DescriptionMaxLength)
            .MinimumLength(JobConstraints.DescriptionMinLength)
            .NotEmpty();

        RuleFor(x => x.ExperienceLevel)
            .IsInEnum();

        RuleFor(x => x.JobType)
            .IsInEnum();

        RuleFor(x => x.LocationType)
            .IsInEnum()
            .NotEmpty();

        RuleFor(x => x.Location)
            .SetValidator(new AddressValidator());

        RuleFor(x => x.MinSalary)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MaxSalary)
            .GreaterThan(x => x.MinSalary);

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
            .NotEmpty();
    }
}