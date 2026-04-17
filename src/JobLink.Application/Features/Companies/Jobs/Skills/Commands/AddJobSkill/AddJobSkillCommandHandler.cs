using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Constants;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobError = JobLink.Application.Features.Jobs.JobError;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.AddJobSkill;

public class AddJobSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<AddJobSkillCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddJobSkillCommand request, CancellationToken cancellationToken)
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

        var job = await dbContext.Jobs
            .Where(x => x.CompanyProfile!.UserId == userId)
            .Select(x => new
            {
                Id = x.Id,
                SkillIds = x.Skills.Select(x => x.SkillId).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == request.JobId, cancellationToken);

        if (job is null)
        {
            return JobError.NotFound;
        }


        if (job.SkillIds.Count >= SkillConstraints.JobSkillCountMax)
        {
            return Error.Conflict($"Job skill count exceeds maximum limit. You can only add {SkillConstraints.JobSkillCountMax} skills");
        }

        if (job.SkillIds.Contains(request.SkillId))
        {
            return Error.Conflict("Job already has this skill");
        }

        var jobSkillResult = JobSkill.Create(job.Id, request.SkillId, request.IsRequired);
        if (jobSkillResult.IsFailure)
        {
            return jobSkillResult.Errors;
        }

        JobSkill jobSkill = jobSkillResult.Value!;

        await dbContext.JobSkills.AddAsync(jobSkill, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return jobSkill.Id;
    }
}
