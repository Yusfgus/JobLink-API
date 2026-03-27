using JobLink.Application.Features.Skills.Commands.UpdateSkill;

namespace JobLink.API.Contracts.Skills;

public sealed record UpdateSkillRequest(string Name)
{
    public UpdateSkillCommand ToCommand(Guid id) 
    { 
        return new UpdateSkillCommand(id, Name);
    }
}