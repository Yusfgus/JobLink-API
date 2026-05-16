using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.Models;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.SavedJobs.Queries.GetMySavedJobs;

public sealed class GetMySavedJobsQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetMySavedJobsQuery, Result<PaginatedList<SavedJobDto>>>
{
    public async Task<Result<PaginatedList<SavedJobDto>>> Handle(GetMySavedJobsQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var query = dbContext.SavedJobs.Where(sj => sj.JobSeekerProfile!.UserId == userId);

        int totalCount = await query.CountAsync(cancellationToken);

        return await query
            .Select(sj => new SavedJobDto(
                sj.Job!.Id,
                sj.Job!.Title,
                sj.Job!.JobType,
                sj.Job.LocationType,
                sj.Job.CompanyProfileId,
                sj.Job.CompanyProfile!.Name,
                sj.Job.CompanyProfile!.LogoUrl,
                sj.Job.Location.Country,
                sj.Job.Location.City,
                sj.Job.Description,
                sj.Job.ExperienceLevel,
                sj.Job.Skills.Select(s => new JobSkillDto(s.Skill!.Id, s.Skill.Name, s.IsRequired)).ToList(),
                sj.Job.PostedAtUtc,
                sj.Job.Applications.Any(a => a.JobSeekerProfile!.UserId == userId),
                sj.SavedAtUtc
            ))
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, cancellationToken);
    }
}
