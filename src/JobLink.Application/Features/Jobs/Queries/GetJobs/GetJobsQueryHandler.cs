using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.Models;
using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JobLink.Application.Features.Jobs.Queries.GetJobs;

public sealed class GetJobsQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetJobsQuery, Result<PaginatedList<JobSummaryDto>>>
{
    public async Task<Result<PaginatedList<JobSummaryDto>>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Jobs.AsNoTracking();

        int totalCount = await query.CountAsync(cancellationToken);

        return await query
            .Select(j => new JobSummaryDto(
                j.Id,
                j.Title,
                j.JobType,
                j.LocationType,
                j.CompanyProfile!.Name,
                j.CompanyProfile!.User!.ProfilePictureUrl,
                j.Location!.Country,
                j.Location!.City,
                j.ExperienceLevel,
                j.Skills.Select(js => js.Skill!.Name).ToList(),
                j.PostedAtUtc
            ))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, cancellationToken);
    }
}