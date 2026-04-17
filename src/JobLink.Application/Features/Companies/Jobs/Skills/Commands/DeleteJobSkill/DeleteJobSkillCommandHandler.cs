using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.DeleteJobSkill;

public class DeleteJobSkillCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<DeleteJobSkillCommand, Result>
{
    public async Task<Result> Handle(DeleteJobSkillCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var affectedRows = await dbContext.JobSkills
            .Where(x =>
                x.Id == request.Id &&
                x.JobId == request.JobId &&
                x.Job!.CompanyProfile!.UserId == userId
            )
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return Error.NotFound("Job skill not found");
        }

        return Result.Success();
    }
}
