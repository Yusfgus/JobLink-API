using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.DeleteEducation;

public class DeleteEducationCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<DeleteEducationCommand, Result>
{
    public async Task<Result> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var affectedRows = await dbContext.Educations
            .Join(
                dbContext.JobSeekerProfiles,
                education => education.JobSeekerProfileId,
                profile => profile.Id,
                (education, profile) => new { education, profile.UserId }
            )
            .Where(x => x.education.Id == request.Id && x.UserId == userId)
            .Select(x => x.education)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return Error.NotFound("Job seeker education not found");
        }

        return Result.Success();
    }
}