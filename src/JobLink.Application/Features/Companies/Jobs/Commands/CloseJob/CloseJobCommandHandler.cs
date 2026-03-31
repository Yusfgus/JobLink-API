using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Jobs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Commands.CloseJob;

public class CloseJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<CloseJobCommand, Result>
{
    public async Task<Result> Handle(CloseJobCommand request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var job = await dbContext.Jobs
            .Join(
                dbContext.CompanyProfiles,
                job => job.CompanyProfileId,
                profile => profile.Id,
                (job, profile) => new { job, profile.UserId }
            )
            .Where(x => x.UserId == userId)
            .Select(x => x.job)
            .FirstOrDefaultAsync(ct);

        if (job is null)
        {
            return JobError.NotFound;
        }

        job.Close();
        await dbContext.SaveChangesAsync(ct);
        return Result.Success();
    }
}
