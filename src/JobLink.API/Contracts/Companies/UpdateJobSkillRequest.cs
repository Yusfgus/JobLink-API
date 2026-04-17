using JobLink.Application.Features.Companies.Jobs.Skills.Commands.UpdateJobSkill;

namespace JobLink.API.Contracts.Companies;

public sealed record UpdateJobSkillRequest(
    Guid? SkillId,
    bool? IsRequired
)
{
    public UpdateJobSkillCommand ToCommand(Guid jobId, Guid id)
    {
        return new UpdateJobSkillCommand(
            jobId,
            id,
            SkillId,
            IsRequired
        );
    }
}
