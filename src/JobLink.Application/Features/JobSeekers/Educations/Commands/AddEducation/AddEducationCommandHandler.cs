using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.AddEducation;

public sealed class AddEducationCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<AddEducationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEducationCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerProfileId = appUser.JobSeekerId;
        if (jobSeekerProfileId is null)
        {
            return JobSeekerError.NotFound;
        }

        var educationResult = Education.Create(
            jobSeekerProfileId.Value,
            request.Degree,
            request.Country,
            request.Institution,
            request.FieldOfStudy,
            request.StartDate,
            request.EndDate,
            request.Grade
        );
        if (educationResult.IsFailure)
        {
            return educationResult.Errors;
        }

        dbContext.Educations.Add(educationResult.Value!);
        await dbContext.SaveChangesAsync(cancellationToken);

        return educationResult.Value!.Id;
    }
}
