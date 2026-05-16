using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Jobs.Queries.GetJobById;

public sealed class GetJobByIdQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetJobByIdQuery, Result<JobDetailsDto>>
{
    public async Task<Result<JobDetailsDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var job = await dbContext.Jobs.AsNoTracking()
            .Where(j => j.Id == request.Id)
            .Select(j => new JobDetailsDto(
                j.Id,
                j.Title,
                j.JobType,
                j.LocationType,
                j.CompanyProfile!.Name,
                j.CompanyProfile!.LogoUrl,
                j.Location!.Country!,
                j.Location!.City!,
                j.Location!.Area,
                j.PostedAtUtc,
                j.ClosedAt,
                j.ExpirationDate,
                j.Status,
                j.ExperienceLevel,
                j.MinSalary,
                j.MaxSalary,
                j.Skills.Select(js => new JobSkillDto(js.Skill!.Id, js.Skill.Name, js.IsRequired)).ToList(),
                j.Description,
                j.Requirements,
                j.Applications.Any(a => a.JobSeekerProfile!.UserId == userId),
                j.Saves.Any(s => s.JobSeekerProfile!.UserId == userId)
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (job is null)
        {
            return JobError.NotFound;
        }

        return job;
    }
}