using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Profile.Commands.UpdateMyJobSeeker;

public class UpdateMyJobSeekerCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateMyJobSeekerCommand, Result>
{
    public async Task<Result> Handle(UpdateMyJobSeekerCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        JobSeekerProfile? jobSeeker = await dbContext.JobSeekerProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        if (jobSeeker is null)
        {
            return JobSeekerError.NotFound;
        }

        var updateResult = jobSeeker.Update(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.MobileNumber,
            request.BirthDate,
            request.Address,
            request.Gender,
            request.Nationality,
            request.MilitaryStatus,
            request.MaritalStatus
        );

        if (updateResult.IsFailure)
        {
            return updateResult.Errors;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}