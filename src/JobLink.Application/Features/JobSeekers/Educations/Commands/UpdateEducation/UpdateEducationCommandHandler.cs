using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;

public sealed class UpdateEducationCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateEducationCommand, Result>
{
    public async Task<Result> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var result = await dbContext.Educations
            .Join(
                dbContext.JobSeekerProfiles,
                education => education.JobSeekerProfileId,
                profile => profile.Id,
                (education, profile) => new { education, profile.UserId }
            )
            .FirstOrDefaultAsync(x => x.education.Id == request.Id && x.UserId == userId, cancellationToken);
        if (result is null)
        {
            return Error.NotFound("Education not found");
        }

        Education education = result.education;

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
