using FluentValidation;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;

public sealed class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

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