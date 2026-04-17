using JobLink.Application.Features.Companies.Jobs.Commands.CreateJob;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.Companies;

public sealed record CreateJobSkillRequest(
    Guid SkillId,
    bool IsRequired
)
{
    public CreateJobSkillCommand ToCommand()
    {
        return new CreateJobSkillCommand(SkillId, IsRequired);
    }
};

public sealed record CreateJobRequest(
    string Title,
    string Description,
    string? Requirements,
    ExperienceLevel ExperienceLevel,
    JobType JobType,
    JobLocationType LocationType,
    string Country,
    string City,
    string? Area,
    int MinSalary,
    int MaxSalary,
    DateOnly ExpirationDate,
    List<CreateJobSkillRequest> Skills
)
{
    public CreateJobCommand ToCommand()
    {
        return new CreateJobCommand(
            Title,
            Description,
            Requirements,
            ExperienceLevel,
            JobType,
            LocationType,
            new (Country, City, Area),
            MinSalary,
            MaxSalary,
            ExpirationDate,
            Skills.Select(s => s.ToCommand()).ToList()
        );
    }
};
