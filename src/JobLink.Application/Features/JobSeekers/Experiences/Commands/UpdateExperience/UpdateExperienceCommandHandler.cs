using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.UpdateExperience;

public sealed class UpdateExperienceCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateExperienceCommand, Result>
{
    public async Task<Result> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerProfileId = appUser.JobSeekerId;
        if (jobSeekerProfileId is null)
        {
            return JobSeekerError.NotFound;
        }

        Experience? experience = await dbContext.Experiences
            .FirstOrDefaultAsync(x => x.Id == request.ExperienceId && x.JobSeekerProfileId == jobSeekerProfileId, cancellationToken);

        if (experience is null)
        {
            return Error.NotFound("Experience.NotFound", "The experience with the specified ID was not found.");
        }

        var updateResult = experience.Update(
            request.Company,
            request.Position,
            request.Country,
            request.Description,
            request.Salary,
            request.StartDate,
            request.EndDate
        );

        if (updateResult.IsFailure)
        {
            return updateResult.Errors;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
