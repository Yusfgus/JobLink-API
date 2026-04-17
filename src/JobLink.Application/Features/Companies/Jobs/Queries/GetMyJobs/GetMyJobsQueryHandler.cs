using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobs;

public sealed class GetMyJobsQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetMyJobsQuery, Result<PaginatedList<CompanyJobSummaryDto>>>
{
    public async Task<Result<PaginatedList<CompanyJobSummaryDto>>> Handle(GetMyJobsQuery request, CancellationToken ct)
    {
        var userId = appUser.UserId;
        if (userId == null)
        {
            return IdentityError.Unauthenticated;
        }

        var query = dbContext.Jobs.AsNoTracking();

        int totalCount = await query.Where(j => j.CompanyProfile!.UserId == userId).CountAsync(ct);

        return await query
            .Where(j => j.CompanyProfile!.UserId == userId)
            .Select(j => new CompanyJobSummaryDto(
                j.Id,
                j.Title,
                j.JobType,
                j.LocationType,
                j.Location!.Country,
                j.Location!.City,
                j.ExperienceLevel,
                j.Skills.Select(js => js.Skill!.Name).ToList(),
                j.PostedAtUtc
            ))
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, ct);
    }
}