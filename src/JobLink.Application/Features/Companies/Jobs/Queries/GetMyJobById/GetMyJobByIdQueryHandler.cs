using System.Data;
using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Jobs;
using JobLink.Application.Features.Jobs.Dtos;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobById;

public sealed class GetMyJobByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyJobByIdQuery, Result<JobDto>>
{
    public async Task<Result<JobDto>> Handle(GetMyJobByIdQuery request, CancellationToken ct)
    {
        var userId = appUser.UserId;
        if (userId == null)
        {
            return CompanyError.NotFound;
        }

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT
                J.Id,
                J.Title,
                J.Description,
                J.Requirements,
                J.ExperienceLevel,
                J.JobType,
                J.LocationType,
                J.Country,
                J.City,
                J.Area,
                J.MinSalary,
                J.MaxSalary,
                J.PostedAtUtc,
                J.ClosedAt,
                J.ExpirationDate,
                J.Status
            FROM Jobs J
            INNER JOIN CompanyProfiles CP ON J.CompanyProfileId = CP.Id
            WHERE CP.UserId = @UserId AND J.Id = @Id
        ";

        JobDto? job = await connection.QueryFirstOrDefaultAsync<JobDto>(sql, new { UserId = userId, Id = request.Id });

        if (job is null)
        {
            return JobError.NotFound;
        }

        return job;
    }
}