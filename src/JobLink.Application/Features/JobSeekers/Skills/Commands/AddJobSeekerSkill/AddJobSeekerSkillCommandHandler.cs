using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Constants;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.AddJobSeekerSkill;

public class AddJobSeekerSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<AddJobSeekerSkillCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddJobSeekerSkillCommand request, CancellationToken cancellationToken)
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

        bool skillExists = await dbContext.Skills.AsNoTracking().AnyAsync(x => x.Id == request.SkillId, cancellationToken);
        if (!skillExists)
        {
            return Error.NotFound("Skill not found");
        }

        int jobSeekerSkillCount = await dbContext.JobSeekerSkills.AsNoTracking().CountAsync(x => x.JobSeekerProfileId == jobSeekerProfileId, cancellationToken);
        if (jobSeekerSkillCount >= SkillConstraints.JobSeekerSkillCountMax)
        {
            return Error.Conflict($"Job seeker skill count exceeds maximum limit. You can only add {SkillConstraints.JobSeekerSkillCountMax} skills");
        }

        bool exists = await dbContext.JobSeekerSkills.AsNoTracking().AnyAsync(x => x.JobSeekerProfileId == jobSeekerProfileId && x.SkillId == request.SkillId, cancellationToken);
        if (exists)
        {
            return Error.Conflict("Job seeker skill already exists");
        }

        var jobSeekerSkillResult = JobSeekerSkill.Create(jobSeekerProfileId.Value, request.SkillId, request.SkillLevel);
        if (jobSeekerSkillResult.IsFailure)
        {
            return jobSeekerSkillResult.Errors;
        }

        JobSeekerSkill jobSeekerSkill = jobSeekerSkillResult.Value!;

        await dbContext.JobSeekerSkills.AddAsync(jobSeekerSkill, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return jobSeekerSkill.Id;
    }
}