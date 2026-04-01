using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Jobs.Queries.GetJobById;

public sealed class GetJobByIdQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetJobByIdQuery, Result<JobDetailsDto>>
{
    public async Task<Result<JobDetailsDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await dbContext.Jobs.AsNoTracking()
            .Where(j => j.Id == request.Id)
            .Select(j => new JobDetailsDto(
                j.Id,
                j.Title,
                j.JobType,
                j.LocationType,
                j.CompanyProfile!.Name,
                j.CompanyProfile!.User!.ProfilePictureUrl,
                j.Location!.Country!,
                j.Location!.City!,
                j.Location!.Area,
                j.PostedAtUtc,
                j.ClosedAt,
                j.ExpirationDate,
                j.Status,
                j.ExperienceLevel,
                j.MinSalary,
                j.MaxSalary,
                j.Skills.Select(js => js.Skill!.Name).ToList(),
                j.Description,
                j.Requirements
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (job is null)
        {
            return JobError.NotFound;
        }

        return job;
    }
}