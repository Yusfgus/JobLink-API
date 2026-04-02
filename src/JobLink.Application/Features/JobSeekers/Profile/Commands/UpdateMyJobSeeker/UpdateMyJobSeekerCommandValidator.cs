using FluentValidation;
using JobLink.Application.Common.Validators;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.JobSeekers.Profile.Commands.UpdateMyJobSeeker;

public class UpdateMyJobSeekerCommandValidator : AbstractValidator<UpdateMyJobSeekerCommand>
{
    public UpdateMyJobSeekerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(JobSeekerProfileConstraints.FirstNameMaxLength)
            .MinimumLength(JobSeekerProfileConstraints.FirstNameMinLength)
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(JobSeekerProfileConstraints.LastNameMaxLength)
            .MinimumLength(JobSeekerProfileConstraints.LastNameMinLength)
            .When(x => x.LastName != null);

        RuleFor(x => x.MiddleName)
            .MaximumLength(JobSeekerProfileConstraints.MiddleNameMaxLength)
            .MinimumLength(JobSeekerProfileConstraints.MiddleNameMinLength)
            .When(x => x.MiddleName != null);

        RuleFor(x => x.MobileNumber)
            .MaximumLength(JobSeekerProfileConstraints.MobileNumberMaxLength)
            .Matches(JobSeekerProfileConstraints.MobileNumberRegex)
            .When(x => x.MobileNumber != null);

        RuleFor(x => x.Nationality)
            .MaximumLength(JobSeekerProfileConstraints.NationalityMaxLength)
            .MinimumLength(JobSeekerProfileConstraints.NationalityMinLength)
            .When(x => x.Nationality != null);

        RuleFor(x => x.Address)
            .SetValidator(new AddressValidator())
            .When(x => x.Address != null);
    }
}