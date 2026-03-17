using MediatR;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Skills;
using JobLink.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Skills.Commands.UpdateSkill;

public sealed class UpdateSkillCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateSkillCommand, Result>
{
    public async Task<Result> Handle(UpdateSkillCommand request, CancellationToken ct)
    {
        Skill? skill = await dbContext.Skills.FindAsync([request.Id], ct);

        if (skill is null)
            return Error.NotFound("Skill not found");

        string skillName = request.Name.Trim().ToLower();

        var skillExists = await dbContext.Skills.AnyAsync(s => s.Id != request.Id && s.Name.ToLower() == skillName, ct);

        if (skillExists)
            return Error.Conflict("Skill already exists");

        var updateResult = skill.Update(skillName);

        if (updateResult.IsFailure)
            return updateResult.Errors;

        await dbContext.SaveChangesAsync(ct);

        Console.WriteLine("Skill updated successfully");

        return Result.Success();
    }
}