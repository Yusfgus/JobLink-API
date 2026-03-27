using JobLink.Application.Features.JobSeekers.Experiences.Commands.AddExperience;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record AddExperienceRequest(
    string Company,
    string Position,
    string Country,
    string? Description,
    int Salary,
    DateOnly StartDate,
    DateOnly EndDate
)
{
    public AddExperienceCommand ToCommand()
    {
        return new AddExperienceCommand(
            Company,
            Position,
            Country,
            Description,
            Salary,
            StartDate,
            EndDate
        );
    }
}
