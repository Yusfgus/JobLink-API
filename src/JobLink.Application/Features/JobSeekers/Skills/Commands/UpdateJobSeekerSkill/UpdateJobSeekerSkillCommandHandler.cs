using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;

public class UpdateJobSeekerSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateJobSeekerSkillCommand, Result>
{
    public async Task<Result> Handle(UpdateJobSeekerSkillCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var result = await (
            from js in dbContext.JobSeekerSkills
            join profile in dbContext.JobSeekerProfiles
                on js.JobSeekerProfileId equals profile.Id
            where js.Id == request.Id && profile.UserId == userId
            select new
            {
                JobSeekerSkill = js,
                SkillExists = dbContext.Skills.Any(s => s.Id == request.SkillId),
                HasDuplicate = dbContext.JobSeekerSkills
                    .Any(x => x.JobSeekerProfileId == js.JobSeekerProfileId
                        && x.SkillId == request.SkillId
                        && x.Id != js.Id)
            }
        ).FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return Error.NotFound("Job seeker skill not found");
        }

        if (result.HasDuplicate)
        {
            return Error.Conflict("Job seeker already has this skill");
        }

        if (!result.SkillExists)
        {
            return Error.NotFound("Skill not found");
        }

        JobSeekerSkill jobSeekerSkill = result.JobSeekerSkill;

        var jobSeekerSkillResult = jobSeekerSkill.Update(request.SkillId, request.SkillLevel);
        if (jobSeekerSkillResult.IsFailure)
        {
            return jobSeekerSkillResult.Errors;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
