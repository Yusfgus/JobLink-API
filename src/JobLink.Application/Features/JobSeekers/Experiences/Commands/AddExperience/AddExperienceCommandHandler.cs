using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.AddExperience;

public sealed class AddExperienceCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<AddExperienceCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddExperienceCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerProfileId = appUser.JobSeekerId;
        if (jobSeekerProfileId is null)
        {
            return JobSeekerError.NotFound;
        }

        var experienceResult = Experience.Create(
            jobSeekerProfileId.Value,
            request.Company,
            request.Position,
            request.Country,
            request.Description,
            request.Salary,
            request.StartDate,
            request.EndDate
        );
        
        if (experienceResult.IsFailure)
        {
            return experienceResult.Errors;
        }

        dbContext.Experiences.Add(experienceResult.Value!);
        await dbContext.SaveChangesAsync(cancellationToken);

        return experienceResult.Value!.Id;
    }
}
