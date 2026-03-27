using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;

public sealed class UpdateEducationCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateEducationCommand, Result>
{
    public async Task<Result> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        Guid? jobSeekerId = appUser.JobSeekerId;
        if (jobSeekerId is null)
        {
            return JobSeekerError.NotFound;
        }

        Education? education = await dbContext.Educations
            .FirstOrDefaultAsync(js => js.Id == request.Id, cancellationToken);

        if (education is null)
        {
            return Error.NotFound("Education not found");
        }

        var educationResult = education.Update(
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

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
