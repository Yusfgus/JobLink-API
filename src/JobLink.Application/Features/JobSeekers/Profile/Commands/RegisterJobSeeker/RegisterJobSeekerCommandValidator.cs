using FluentValidation;
using JobLink.Domain.Common.Constants;
using JobLink.Application.Common.Validators;

namespace JobLink.Application.Features.JobSeekers.Profile.Commands.RegisterJobSeeker;

public sealed class RegisterJobSeekerCommandValidator : AbstractValidator<RegisterJobSeekerCommand>
{
    public RegisterJobSeekerCommandValidator()
    {
        RuleFor(x => x.User)
            .SetValidator(new RegisterUserValidator());

        RuleFor(x => x.FirstName)
            .MinimumLength(JobSeekerProfileConstraints.FirstNameMinLength)
            .MaximumLength(JobSeekerProfileConstraints.FirstNameMaxLength)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .MinimumLength(JobSeekerProfileConstraints.LastNameMinLength)
            .MaximumLength(JobSeekerProfileConstraints.LastNameMaxLength)
            .NotEmpty();

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender");
    }
}