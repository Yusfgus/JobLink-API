using System.Data;
using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.Models;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplications;

public sealed class GetMyApplicationsQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetMyApplicationsQuery, Result<PaginatedList<JobApplicationSummaryDto>>>
{
    public async Task<Result<PaginatedList<JobApplicationSummaryDto>>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var query = dbContext.JobApplications.Where(ja => ja.JobSeekerProfile!.UserId == userId);

        int totalCount = await query.CountAsync(cancellationToken);

        return await query
            .Select(ja => new JobApplicationSummaryDto(
                ja.Id,
                ja.Job!.Id,
                ja.Job!.Title,
                ja.Job.CompanyProfileId,
                ja.Job.CompanyProfile!.Name,
                ja.Job.CompanyProfile!.User!.ProfilePictureUrl,
                $"{ja.Job.Location.Country} - {ja.Job.Location.City}",
                ja.Status,
                ja.AppliedAtUtc
            ))
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, cancellationToken);
    }
}