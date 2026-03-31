using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Commands.UpdateJob;

public class UpdateJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateJobCommand, Result>
{
    public async Task<Result> Handle(UpdateJobCommand request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;
        if (userId == null)
        {
            return IdentityError.Unauthenticated;
        }

        Job? job = await dbContext.Jobs
            .Join(
                dbContext.CompanyProfiles,
                job => job.CompanyProfileId,
                companyProfile => companyProfile.Id,
                (job, companyProfile) => new { job, companyProfile.UserId }
            )
            .Where(j => j.UserId == userId)
            .Select(j => j.job)
            .FirstOrDefaultAsync(j => j.Id == request.Id, ct);

        if (job == null)
        {
            return Features.Jobs.JobError.NotFound;
        }

        Result result = job.Update(request.Title,
            request.Description,
            request.Requirements,
            request.JobType,
            request.LocationType,
            request.Country,
            request.City,
            request.Area,
            request.MinSalary,
            request.MaxSalary,
            request.ExperienceLevel,
            request.ExpirationDate
        );
        if (result.IsFailure)
        {
            return result;
        }

        await dbContext.SaveChangesAsync(ct);

        return Result.Success();
    }
}
