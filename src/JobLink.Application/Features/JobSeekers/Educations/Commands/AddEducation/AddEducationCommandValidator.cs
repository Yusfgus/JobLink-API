using FluentValidation;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.AddEducation;

public sealed class AddEducationCommandValidator : AbstractValidator<AddEducationCommand>
{
    public AddEducationCommandValidator()
    {
        RuleFor(x => x.Degree)
            .MaximumLength(EducationConstraints.DegreeMaxLength)
            .MinimumLength(EducationConstraints.DegreeMinLength)
            .NotEmpty();

        RuleFor(x => x.Country)
            .MaximumLength(EducationConstraints.CountryMaxLength)
            .MinimumLength(EducationConstraints.CountryMinLength)
            .NotEmpty();

        RuleFor(x => x.Institution)
            .MaximumLength(EducationConstraints.InstitutionMaxLength)
            .MinimumLength(EducationConstraints.InstitutionMinLength)
            .NotEmpty();

        RuleFor(x => x.FieldOfStudy)
            .MaximumLength(EducationConstraints.FieldOfStudyMaxLength)
            .MinimumLength(EducationConstraints.FieldOfStudyMinLength)
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