using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Jobs;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobById;

public sealed class GetMyJobByIdQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetMyJobByIdQuery, Result<CompanyJobDetailsDto>>
{
    public async Task<Result<CompanyJobDetailsDto>> Handle(GetMyJobByIdQuery request, CancellationToken ct)
    {
        var userId = appUser.UserId;
        if (userId == null)
        {
            return CompanyError.NotFound;
        }

        var job = await dbContext.Jobs.AsNoTracking()
            .Where(j => j.Id == request.Id && j.CompanyProfile!.UserId == userId)
            .Select(j => new CompanyJobDetailsDto(
                j.Id,
                j.Title,
                j.JobType,
                j.LocationType,
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
                j.Skills.Select(js => new CompanyJobSkillDto(js.Id, js.Skill!.Name, js.IsRequired)).ToList(),
                j.Description,
                j.Requirements
            ))
            .FirstOrDefaultAsync(ct);

        if (job is null)
        {
            return JobError.NotFound;
        }

        return job;
    }
}