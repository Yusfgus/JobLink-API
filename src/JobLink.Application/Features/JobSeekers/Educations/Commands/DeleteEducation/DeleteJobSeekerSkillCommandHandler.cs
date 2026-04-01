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
            .Where(e =>
                e.Id == request.Id &&
                e.JobSeekerProfile!.UserId == userId
            )
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return Error.NotFound("Job seeker education not found");
        }

        return Result.Success();
    }
}