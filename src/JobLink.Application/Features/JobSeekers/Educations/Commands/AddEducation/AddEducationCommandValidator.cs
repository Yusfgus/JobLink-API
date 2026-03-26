using FluentValidation;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.AddEducation;

public sealed class AddEducationCommandValidator : AbstractValidator<AddEducationCommand>
{
    public AddEducationCommandValidator()
    {
        RuleFor(x => x.Degree)
            .NotEmpty();

        RuleFor(x => x.Country)
            .NotEmpty();

        RuleFor(x => x.Institution)
            .NotEmpty();

        RuleFor(x => x.FieldOfStudy)
            .NotEmpty();

        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThan(x => x.StartDate);

        RuleFor(x => x.Grade)
            .IsInEnum();
    }
}