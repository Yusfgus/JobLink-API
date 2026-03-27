using JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record UpdateEducationRequest(
    string? Degree,
    string? Country,
    string? Institution,
    string? FieldOfStudy,
    DateOnly? StartDate,
    DateOnly? EndDate,
    AcademicGrade? Grade
)
{
    public UpdateEducationCommand ToCommand(Guid educationId)
    {
        return new UpdateEducationCommand(
            educationId,
            Degree,
            Country,
            Institution,
            FieldOfStudy,
            StartDate,
            EndDate,
            Grade
        );
    }
};
