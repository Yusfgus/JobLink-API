using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
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
            // do something
            return IdentityError.UserNotFound;
        }

        Guid? jobSeekerProfileId = appUser.JobSeekerId;
        if (jobSeekerProfileId is null)
        {
            return Error.NotFound("Job seeker profile not found");
        }

        JobSeekerSkill? jobSeekerSkill = await dbContext.JobSeekerSkills.FirstOrDefaultAsync(x => x.Id == request.JobSeekerSkillId && x.JobSeekerProfileId == jobSeekerProfileId, cancellationToken);
        if (jobSeekerSkill is null)
        {
            return Error.NotFound("Job seeker skill not found");
        }

        dbContext.JobSeekerSkills.Remove(jobSeekerSkill);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}