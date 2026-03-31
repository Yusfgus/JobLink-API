using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Commands.CreateJob;

public sealed class CreateJobCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<CreateJobCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var companyProfileId = appUser.CompanyId;
        if (companyProfileId == null)
        {
            return CompanyError.NotFound;
        }

        var jobResult = Job.Create(
            companyProfileId.Value,
            request.Title,
            request.Description,
            request.Requirements,
            request.JobType,
            request.LocationType,
            request.Location,
            request.MinSalary,
            request.MaxSalary,
            request.ExperienceLevel,
            request.ExpirationDate,
            JobStatus.Published
        );

        if (jobResult.IsFailure)
        {
            return jobResult.Errors;
        }

        dbContext.Jobs.Add(jobResult.Value!);
        await dbContext.SaveChangesAsync(cancellationToken);

        return jobResult.Value!.Id;
    }
}