using JobLink.Application.Features.JobSeekers.Skills.Commands.AddJobSeekerSkill;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record AddJobSeekerSkillRequest(
    Guid SkillId,
    SkillLevel Level
)
{
    public AddJobSeekerSkillCommand ToCommand()
    {
        return new AddJobSeekerSkillCommand(
            SkillId,
            Level
        );
    }
}
