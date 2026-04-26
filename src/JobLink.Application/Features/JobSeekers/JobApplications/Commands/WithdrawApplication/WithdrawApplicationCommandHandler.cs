using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobApplications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Commands.WithdrawApplication;

public class WithdrawApplicationCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<WithdrawApplicationCommand, Result>
{
    public async Task<Result> Handle(WithdrawApplicationCommand request, CancellationToken cancellationToken)
    {
        // get current user id
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        JobApplication? jobApplication = await dbContext.JobApplications
            .FirstOrDefaultAsync(ja =>
                ja.JobId == request.Id &&
                ja.JobSeekerProfile!.UserId == userId,
                cancellationToken
            );
        if (jobApplication is null)
        {
            return Error.NotFound("Job application not found");
        }

        // remove the job application
        dbContext.JobApplications.Remove(jobApplication);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}