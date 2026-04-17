using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.UpdateJobSkill;

public class UpdateJobSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateJobSkillCommand, Result>
{
    public async Task<Result> Handle(UpdateJobSkillCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        JobSkill? jobSkill = await dbContext.JobSkills
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                x.JobId == request.JobId &&
                x.Job!.CompanyProfile!.UserId == userId,
                cancellationToken);

        if (jobSkill is null)
        {
            return Error.NotFound("Job skill not found");
        }

        if (request.SkillId.HasValue)
        {
            // Check if new skill exists
            bool skillExists = await dbContext.Skills.AnyAsync(x => x.Id == request.SkillId, cancellationToken);
            if (!skillExists)
            {
                return Error.NotFound("Skill not found");
            }

            // Check for duplicate
            bool hasDuplicate = await dbContext.JobSkills
                .AnyAsync(x => x.JobId == jobSkill.JobId
                    && x.SkillId == request.SkillId
                    && x.Id != request.Id, cancellationToken);
            if (hasDuplicate)
            {
                return Error.Conflict("Job already has this skill");
            }
        }

        var result = jobSkill.Update(request.SkillId, request.IsRequired);
        if (result.IsFailure)
        {
            return result.Errors;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
