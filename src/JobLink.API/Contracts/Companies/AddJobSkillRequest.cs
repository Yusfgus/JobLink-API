using JobLink.Application.Features.Companies.Jobs.Skills.Commands.AddJobSkill;

namespace JobLink.API.Contracts.Companies;

public sealed record AddJobSkillRequest(
    Guid SkillId,
    bool IsRequired
)
{
    public AddJobSkillCommand ToCommand(Guid jobId)
    {
        return new AddJobSkillCommand(
            jobId,
            SkillId,
            IsRequired
        );
    }
}
