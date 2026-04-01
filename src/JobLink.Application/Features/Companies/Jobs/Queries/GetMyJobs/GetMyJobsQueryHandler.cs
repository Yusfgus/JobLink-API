using System.Data;
using Dapper;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.Models;

namespace JobLink.Application.Features.Companies.Jobs.Queries.GetMyJobs;

public sealed class GetMyJobsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyJobsQuery, Result<PaginatedList<CompanyJobDto>>>
{
    public async Task<Result<PaginatedList<CompanyJobDto>>> Handle(GetMyJobsQuery request, CancellationToken ct)
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
            LIMIT @PageSize OFFSET @Offset
        ";

        IEnumerable<CompanyJobDto> jobs = await connection.QueryAsync<CompanyJobDto>(sql, new { UserId = userId, PageSize = request.PageSize, Offset = (request.PageNumber - 1) * request.PageSize });

        int totalCount = await connection.QuerySingleAsync<int>(@"SELECT COUNT(*) FROM Jobs J INNER JOIN CompanyProfiles CP ON J.CompanyProfileId = CP.Id WHERE CP.UserId = @UserId", new { UserId = userId });

        return new PaginatedList<CompanyJobDto>(request.PageNumber, request.PageSize, totalCount, jobs.ToList());
    }
}