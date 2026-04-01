using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.DeleteJobSeekerSkill;

public class DeleteJobSeekerSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<DeleteJobSeekerSkillCommand, Result>
{
    public async Task<Result> Handle(DeleteJobSeekerSkillCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var affectedRows = await dbContext.JobSeekerSkills
            .Where(x =>
                x.Id == request.Id &&
                x.JobSeekerProfile!.UserId == userId
            )
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return Error.NotFound("Job seeker skill not found");
        }

        return Result.Success();
    }
}