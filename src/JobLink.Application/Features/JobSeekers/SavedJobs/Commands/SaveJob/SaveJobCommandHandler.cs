using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Jobs;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.SavedJobs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.SavedJobs.Commands.SaveJob;

public class SaveJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<SaveJobCommand, Result>
{
    public async Task<Result> Handle(SaveJobCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerId = appUser.JobSeekerId;
        if (jobSeekerId is null)
        {
            return JobSeekerError.NotFound;
        }

        var job = await dbContext.Jobs.FindAsync([request.JobId], cancellationToken);
        if (job is null)
        {
            return JobError.NotFound;
        }

        // if (job.Status != JobStatus.Opened)
        // {
        //     return JobError.NotAvailable;
        // }

        var existingSavedJob = await dbContext.SavedJobs
            .FirstOrDefaultAsync(x => x.JobSeekerProfileId == jobSeekerId.Value && x.JobId == request.JobId, cancellationToken);
        if (existingSavedJob is not null)
        {
            return SavedJobError.AlreadySaved;
        }

        var savedJobResult = SavedJob.Create(jobSeekerId.Value, request.JobId);
        if (savedJobResult.IsFailure)
        {
            return savedJobResult.Errors;
        }

        await dbContext.SavedJobs.AddAsync(savedJobResult.Value!, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
