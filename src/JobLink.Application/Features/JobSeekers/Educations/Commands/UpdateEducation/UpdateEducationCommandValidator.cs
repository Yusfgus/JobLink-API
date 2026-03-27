using FluentValidation;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;

public sealed class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        // validate if not null
        RuleFor(x => x.Degree)
            .MaximumLength(EducationConstraints.DegreeMaxLength)
            .MinimumLength(EducationConstraints.DegreeMinLength)
            .When(x => x.Degree != null);

        RuleFor(x => x.Country)
            .MaximumLength(EducationConstraints.CountryMaxLength)
            .MinimumLength(EducationConstraints.CountryMinLength)
            .When(x => x.Country != null);

        RuleFor(x => x.Institution)
            .MaximumLength(EducationConstraints.InstitutionMaxLength)
            .MinimumLength(EducationConstraints.InstitutionMinLength)
            .When(x => x.Institution != null);

        RuleFor(x => x.FieldOfStudy)
            .MaximumLength(EducationConstraints.FieldOfStudyMaxLength)
            .MinimumLength(EducationConstraints.FieldOfStudyMinLength)
            .When(x => x.FieldOfStudy != null);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate != null && x.StartDate != null);

        RuleFor(x => x.Grade)
            .IsInEnum()
            .When(x => x.Grade != null);
    }
}