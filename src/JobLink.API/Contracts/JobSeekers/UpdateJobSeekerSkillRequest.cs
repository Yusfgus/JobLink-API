using JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record UpdateJobSeekerSkillRequest(
    Guid? SkillId,
    SkillLevel? Level
)
{
    public UpdateJobSeekerSkillCommand ToCommand(Guid jobSeekerSkillId)
    {
        return new UpdateJobSeekerSkillCommand(
            jobSeekerSkillId,
            SkillId,
            Level
        );
    }
}
