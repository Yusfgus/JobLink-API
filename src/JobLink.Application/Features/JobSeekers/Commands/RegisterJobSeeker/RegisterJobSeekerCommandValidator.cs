using FluentValidation;
using JobLink.Domain.Common.Constants;
using JobLink.Application.Common.Validators;

namespace JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;

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

        // RuleFor(x => x.MiddleName)
        //     .MinimumLength(JobSeekerProfileConstraints.MiddleNameMinLength)
        //     .MaximumLength(JobSeekerProfileConstraints.MiddleNameMaxLength)
        //     .When(x => !string.IsNullOrEmpty(x.MiddleName));

        RuleFor(x => x.LastName)
            .MinimumLength(JobSeekerProfileConstraints.LastNameMinLength)
            .MaximumLength(JobSeekerProfileConstraints.LastNameMaxLength)
            .NotEmpty();

        // RuleFor(x => x.MobileNumber)
        //     .MaximumLength(JobSeekerProfileConstraints.MobileNumberMaxLength)
        //     .Matches(JobSeekerProfileConstraints.MobileNumberRegex).WithMessage("Invalid phone number");

        // RuleFor(x => x.BirthDate)
        //     .Must(x => x <= DateOnly.FromDateTime(DateTime.Now)).WithMessage("Invalid birth date");

        // RuleFor(x => x.Address)
        //     .SetValidator(new AddressValidator());

        // RuleFor(x => x.Gender)
        //     .IsInEnum().WithMessage("Invalid gender");

        // RuleFor(x => x.Nationality)
        //     .MaximumLength(JobSeekerProfileConstraints.NationalityMaxLength);

        // RuleFor(x => x.MilitaryStatus)
        //     .IsInEnum().WithMessage("Invalid military status");

        // RuleFor(x => x.MaritalStatus)
        //     .IsInEnum().WithMessage("Invalid marital status");
    }
}