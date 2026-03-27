using JobLink.Application.Features.JobSeekers.Experiences.Commands.UpdateExperience;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record UpdateExperienceRequest(
    string? Company,
    string? Position,
    string? Country,
    string? Description,
    int? Salary,
    DateOnly? StartDate,
    DateOnly? EndDate
)
{
    public UpdateExperienceCommand ToCommand(Guid experienceId)
    {
        return new UpdateExperienceCommand(
            experienceId,
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
