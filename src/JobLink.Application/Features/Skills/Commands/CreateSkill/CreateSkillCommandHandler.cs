using MediatR;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Skills;
using JobLink.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Skills.Commands.CreateSkill;

public sealed class CreateSkillCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateSkillCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateSkillCommand request, CancellationToken ct)
    {
        string skillName = request.Name.Trim().ToLower();

        var skillExists = await dbContext.Skills.AnyAsync(s => s.Name.ToLower() == skillName, ct);

        if (skillExists)
            return Error.Conflict("Skill already exists");

        var skillResult = Skill.Create(skillName);

        if (skillResult.IsFailure)
            return skillResult.Errors;

        Skill skill = skillResult.Value!;

        await dbContext.Skills.AddAsync(skill, ct);

        await dbContext.SaveChangesAsync(ct);

        return skill.Id;
    }
}