using JobLink.Application.Features.Companies.Jobs.Commands.UpdateJob;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.Companies;

public sealed record UpdateJobRequest(
    string? Title,
    string? Description,
    string? Requirements,
    ExperienceLevel? ExperienceLevel,
    JobType? JobType,
    JobLocationType? LocationType,
    string? Country,
    string? City,
    string? Area,
    int? MinSalary,
    int? MaxSalary,
    DateOnly? ExpirationDate
)
{
    public UpdateJobCommand ToCommand(Guid id)
    {
        return new UpdateJobCommand(
            id,
            Title,
            Description,
            Requirements,
            ExperienceLevel,
            JobType,
            LocationType,
            Country,
            City,
            Area,
            MinSalary,
            MaxSalary,
            ExpirationDate
        );
    }
};