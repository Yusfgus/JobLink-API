using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.Models;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Jobs.Queries.GetJobs;

public sealed class GetJobsQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetJobsQuery, Result<PaginatedList<JobSummaryDto>>>
{
    public async Task<Result<PaginatedList<JobSummaryDto>>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var query = dbContext.Jobs.AsNoTracking();

        int totalCount = await query.CountAsync(cancellationToken);

        return await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(j => new JobSummaryDto(
                j.Id,
                j.Title,
                j.JobType,
                j.LocationType,
                j.CompanyProfile!.Name,
                j.CompanyProfile!.LogoUrl,
                j.Location!.Country,
                j.Location!.City,
                j.Description,
                j.ExperienceLevel,
                j.Skills.Select(js => js.Skill!.Name).ToList(),
                j.PostedAtUtc,
                j.Applications.Any(a => a.JobSeekerProfile!.UserId == userId),
                j.Saves.Any(s => s.JobSeekerProfile!.UserId == userId)
            ))
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, cancellationToken);
    }
}