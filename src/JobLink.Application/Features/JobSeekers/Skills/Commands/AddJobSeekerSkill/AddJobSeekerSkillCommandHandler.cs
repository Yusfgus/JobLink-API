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
            return IdentityError.Unauthenticated;
        }

        bool skillExists = await dbContext.Skills.AnyAsync(x => x.Id == request.SkillId, cancellationToken);
        if (!skillExists)
        {
            return Error.NotFound("Skill not found");
        }

        var jobSeekerProfile = await dbContext.JobSeekerProfiles
            .Select(x => new
            {
                Id = x.Id,
                UserId = x.UserId,
                SkillIds = x.Skills.Select(x => x.SkillId).ToList()
            })
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (jobSeekerProfile is null)
        {
            return JobSeekerError.NotFound;
        }

        if (jobSeekerProfile.SkillIds.Count >= SkillConstraints.JobSeekerSkillCountMax)
        {
            return Error.Conflict($"Job seeker skill count exceeds maximum limit. You can only add {SkillConstraints.JobSeekerSkillCountMax} skills");
        }

        if (jobSeekerProfile.SkillIds.Contains(request.SkillId))
        {
            return Error.Conflict("Job seeker already has this skill");
        }

        var jobSeekerSkillResult = JobSeekerSkill.Create(jobSeekerProfile.Id, request.SkillId, request.SkillLevel);
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