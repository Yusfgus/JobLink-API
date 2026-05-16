using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Queries.GetMyApplicationById;

public sealed class GetMyApplicationByIdQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetMyApplicationByIdQuery, Result<JobApplicationDetailsDto>>
{
    public async Task<Result<JobApplicationDetailsDto>> Handle(GetMyApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var job = await dbContext.JobApplications
            .Where(ja =>
                ja.JobSeekerProfile!.UserId == userId &&
                ja.JobId == request.Id
            )
            .Select(ja => new JobApplicationDetailsDto(
                ja.Job!.Id,
                ja.Job!.Title,
                ja.Job.CompanyProfileId,
                ja.Job.CompanyProfile!.Name,
                ja.Job.Location.Country,
                ja.Job.Location.City,
                ja.Status,
                ja.AppliedAtUtc
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (job is null)
        {
            return Error.NotFound("JobApplication.NotFound", "Job application not found");
        }

        return job;
    }
}