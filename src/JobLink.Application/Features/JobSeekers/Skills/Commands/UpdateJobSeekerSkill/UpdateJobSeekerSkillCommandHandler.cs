using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;

public class UpdateJobSeekerSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateJobSeekerSkillCommand, Result>
{
    public async Task<Result> Handle(UpdateJobSeekerSkillCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerId = appUser.JobSeekerId;
        if (jobSeekerId is null)
        {
            return JobSeekerError.NotFound;
        }

        JobSeekerSkill? jobSeekerSkill = await dbContext.JobSeekerSkills
            .FirstOrDefaultAsync(js => js.Id == request.Id, cancellationToken);
        if (jobSeekerSkill is null)
        {
            return Error.NotFound("Job seeker skill not found");
        }
        
        if (request.SkillId.HasValue)
        {
            // Check if skill exists
            var skillExists = await dbContext.Skills.AnyAsync(s => s.Id == request.SkillId, cancellationToken);
            if (!skillExists)
            {
                return Error.NotFound("Skill not found");
            }

            // Check for duplicate
            var hasDuplicate = await dbContext.JobSeekerSkills
                .AnyAsync(x => x.JobSeekerProfileId == jobSeekerSkill.JobSeekerProfileId
                    && x.SkillId == request.SkillId
                    && x.Id != request.Id, cancellationToken);
            if (hasDuplicate)
            {
                return Error.Conflict("Job seeker already has this skill");
            }
        }
        
        var jobSeekerSkillResult = jobSeekerSkill.Update(request.SkillId, request.SkillLevel);
        if (jobSeekerSkillResult.IsFailure)
        {
            return jobSeekerSkillResult.Errors;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
