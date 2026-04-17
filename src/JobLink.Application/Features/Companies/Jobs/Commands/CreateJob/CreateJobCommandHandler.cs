using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Commands.CreateJob;

public sealed class CreateJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<CreateJobCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var companyProfileId = appUser.CompanyId;
        if (companyProfileId == null)
        {
            return CompanyError.NotFound;
        }

        // check if all skills exist
        var distinctSkills = request.Skills.DistinctBy(s => s.SkillId).ToList();
        var distinctIds = distinctSkills.Select(s => s.SkillId).ToHashSet();

        var skillsIds = await dbContext.Skills.AsNoTracking()
            .Select(s => s.Id)
            .Where(id => distinctIds.Contains(id))
            .ToListAsync(cancellationToken);

        if (skillsIds.Count != distinctIds.Count)
        {
            return Error.Validation("Job_Skills_NotFound", "Skills not found");
        }

        var jobResult = Job.Create(
            companyProfileId.Value,
            request.Title,
            request.Description,
            request.Requirements,
            request.JobType,
            request.LocationType,
            request.Location,
            request.MinSalary,
            request.MaxSalary,
            request.ExperienceLevel,
            request.ExpirationDate
        );
        if (jobResult.IsFailure)
        {
            return jobResult.Errors;
        }

        var job = jobResult.Value!;

        var jobSkills = new List<JobSkill>();
        foreach (var skill in distinctSkills)
        {
            var jobSkillResult = JobSkill.Create(job.Id, skill.SkillId, skill.IsRequired);
            if (jobSkillResult.IsFailure)
            {
                return jobSkillResult.Errors;
            }
            jobSkills.Add(jobSkillResult.Value!);
        }

        dbContext.Jobs.Add(job);
        dbContext.JobSkills.AddRange(jobSkills);
        await dbContext.SaveChangesAsync(cancellationToken);

        return job.Id;
    }
}