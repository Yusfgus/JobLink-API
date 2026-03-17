using JobLink.API.Contracts.Skills;
using JobLink.Application.Features.Skills.Commands.CreateSkill;
using JobLink.Application.Features.Skills.Commands.UpdateSkill;

namespace JobLink.API.Mappings;

public static class SkillMappings
{
    public static CreateSkillCommand ToCommand(this CreateSkillRequest request) 
    { 
        return new CreateSkillCommand(request.Name);
    }

    public static UpdateSkillCommand ToCommand(this UpdateSkillRequest request, Guid id) 
    { 
        return new UpdateSkillCommand(id, request.Name);
    }
}