using JobLink.Application.Features.Skills.Commands.CreateSkill;

namespace JobLink.API.Contracts.Skills;

public sealed record CreateSkillRequest(string Name)
{
    public CreateSkillCommand ToCommand() 
    { 
        return new CreateSkillCommand(Name);
    }
}
