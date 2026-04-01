using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Jobs;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobApplications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Commands.ApplyForJob;

public class ApplyForJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<ApplyForJobCommand, Result>
{
    public async Task<Result> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        // get current user id
        Guid? jobSeekerId = appUser.JobSeekerId;
        if (jobSeekerId is null)
        {
            return JobSeekerError.NotFound;
        }

        // get job by id
        var job = await dbContext.Jobs.FindAsync([request.JobId], cancellationToken);
        if (job is null)
        {
            return JobError.NotFound;
        }

        if (job.Status != JobStatus.Opened)
        {
            return JobError.NotAvailable;
        }

        // check for duplication
        var existingApplication = await dbContext.JobApplications
            .FirstOrDefaultAsync(x => x.JobSeekerProfileId == jobSeekerId.Value && x.JobId == request.JobId, cancellationToken);
        if (existingApplication is not null)
        {
            return JobApplicationError.AlreadyApplied;
        }

        var jobApplicationResult = JobApplication.Create(jobSeekerId.Value, request.JobId);
        if (jobApplicationResult.IsFailure)
        {
            return jobApplicationResult.Errors;
        }

        await dbContext.JobApplications.AddAsync(jobApplicationResult.Value!, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}