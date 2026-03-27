using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.DeleteExperience;

public sealed class DeleteExperienceCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<DeleteExperienceCommand, Result>
{
    public async Task<Result> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerProfileId = appUser.JobSeekerId;
        if (jobSeekerProfileId is null)
        {
            return JobSeekerError.NotFound;
        }

        int deletedRows = await dbContext.Experiences
            .Where(x => x.Id == request.Id && x.JobSeekerProfileId == jobSeekerProfileId)
            .ExecuteDeleteAsync(cancellationToken);

        if (deletedRows == 0)
        {
            return Error.NotFound("Experience.NotFound", "The experience with the specified ID was not found.");
        }

        return Result.Success();
    }
}
