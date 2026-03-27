using JobLink.Application.Features.JobSeekers.Educations.Commands.AddEducation;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record AddEducationRequest(
    string Degree,
    string Country,
    string Institution,
    string FieldOfStudy,
    DateOnly StartDate,
    DateOnly EndDate,
    AcademicGrade Grade
)
{
    public AddEducationCommand ToCommand()
    {
        return new AddEducationCommand(
            Degree,
            Country,
            Institution,
            FieldOfStudy,
            StartDate,
            EndDate,
            Grade
        );
    }
}
