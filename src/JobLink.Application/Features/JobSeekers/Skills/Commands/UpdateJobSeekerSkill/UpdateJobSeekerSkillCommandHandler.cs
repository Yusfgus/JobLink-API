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
            // do something
            return IdentityError.UserNotFound;
        }

        Guid? jobSeekerProfileId = appUser.JobSeekerId;
        if (jobSeekerProfileId is null)
        {
            return Error.NotFound("Job seeker profile not found");
        }

        JobSeekerSkill? jobSeekerSkill = await dbContext.JobSeekerSkills.FirstOrDefaultAsync(x => x.Id == request.JobSeekerSkillId && x.JobSeekerProfileId == jobSeekerProfileId, cancellationToken);
        if (jobSeekerSkill is null)
        {
            return Error.NotFound("Job seeker skill not found");
        }

        bool skillExists = await dbContext.Skills.AsNoTracking().AnyAsync(x => x.Id == request.SkillId, cancellationToken);
        if (!skillExists)
        {
            return Error.NotFound("Skill not found");
        }

        bool exists = await dbContext.JobSeekerSkills.AsNoTracking().AnyAsync(
            x => x.JobSeekerProfileId == jobSeekerProfileId 
            && x.SkillId == request.SkillId 
            && x.Id != request.JobSeekerSkillId, 
            cancellationToken);
        if (exists)
        {
            return Error.Conflict("Job seeker skill already exists");
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
