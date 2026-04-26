using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.SavedJobs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.SavedJobs.Commands.UnsaveJob;

public class UnsaveJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UnsaveJobCommand, Result>
{
    public async Task<Result> Handle(UnsaveJobCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        SavedJob? savedJob = await dbContext.SavedJobs
            .FirstOrDefaultAsync(sj =>
                sj.JobId == request.JobId &&
                sj.JobSeekerProfile!.UserId == userId,
                cancellationToken
            );

        if (savedJob is null)
        {
            return SavedJobError.NotFound;
        }

        dbContext.SavedJobs.Remove(savedJob);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
