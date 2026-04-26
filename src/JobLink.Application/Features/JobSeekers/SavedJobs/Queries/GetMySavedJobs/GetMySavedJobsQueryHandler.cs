using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.Models;
using JobLink.Application.Features.Identity;
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
            .LeftJoin(
                dbContext.JobApplications.Where(ja => ja.JobSeekerProfile!.UserId == userId),
                sj => sj.JobId,
                ja => ja.JobId,
                (sj, ja) => new SavedJobDto(
                    sj.Id,
                    sj.Job!.Id,
                    sj.Job!.Title,
                    sj.Job.CompanyProfileId,
                    sj.Job.CompanyProfile!.Name,
                    sj.Job.CompanyProfile!.LogoUrl,
                    $"{sj.Job.Location.Country} - {sj.Job.Location.City}",
                    sj.SavedAtUtc,
                    ja != null
                )
            )
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, cancellationToken);
    }
}
