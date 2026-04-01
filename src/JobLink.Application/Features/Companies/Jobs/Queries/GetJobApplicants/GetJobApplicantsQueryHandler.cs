using System.Data;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.Models;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Jobs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetJobApplicants;

public class GetJobApplicantsQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetJobApplicantsQuery, Result<PaginatedList<JobApplicantsDto>>>
{
    public async Task<Result<PaginatedList<JobApplicantsDto>>> Handle(GetJobApplicantsQuery request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;
        if (userId == null)
        {
            return IdentityError.Unauthenticated;
        }

        bool jobExists = await dbContext.Jobs
            .AnyAsync(j =>
                j.Id == request.JobId &&
                j.CompanyProfile!.User!.Id == userId,
                ct);

        if (!jobExists)
        {
            return JobError.NotFound;
        }

        int totalCount = await dbContext.JobApplications
            .CountAsync(ja => ja.JobId == request.JobId, ct);

        return await dbContext.JobApplications.AsNoTracking()
            .Where(ja => ja.JobId == request.JobId)
            .Select(ja => new JobApplicantsDto(
                ja.Id,
                ja.JobSeekerProfileId,
                ja.JobSeekerProfile!.FirstName + " " + ja.JobSeekerProfile.LastName,
                ja.JobSeekerProfile!.User!.Email,
                ja.JobSeekerProfile!.MobileNumber,
                ja.JobSeekerProfile!.User!.ProfilePictureUrl,
                ja.JobSeekerProfile!.Resume!.FileUrl,
                ja.AppliedAtUtc,
                ja.Status
            ))
            .ToPaginatedListAsync(request.Page, request.PageSize, totalCount, ct);
    }
}