using System.Data;
using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Jobs.Dtos;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobs;

public sealed class GetMyJobsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyJobsQuery, Result<IEnumerable<JobDto>>>
{
    public async Task<Result<IEnumerable<JobDto>>> Handle(GetMyJobsQuery request, CancellationToken ct)
    {
        var userId = appUser.UserId;
        if (userId == null)
        {
            return IdentityError.Unauthenticated;
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
            WHERE CP.UserId = @UserId
        ";

        IEnumerable<JobDto> jobs = await connection.QueryAsync<JobDto>(sql, new { UserId = userId });

        return jobs.ToList();
    }
}