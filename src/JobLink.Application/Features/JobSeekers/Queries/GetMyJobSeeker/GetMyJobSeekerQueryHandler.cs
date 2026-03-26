using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.JobSeekers.Queries.GetMyJobSeeker;

public class GetMyJobSeekerQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyJobSeekerQuery, Result<JobSeekerProfileDto>>
{
    public async Task<Result<JobSeekerProfileDto>> Handle(GetMyJobSeekerQuery request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;

        if (userId is null)
        {
            // do something
            return IdentityError.Unauthenticated;
        }

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT 
                JP.Id,
                JP.FirstName,
                JP.MiddleName,
                JP.LastName,
                U.Email,
                JP.MobileNumber,
                JP.BirthDate,
                JP.Gender,
                JP.Nationality,
                JP.MilitaryStatus,
                JP.MaritalStatus,
                JP.Country,
                JP.City,
                JP.Area
            FROM JobSeekerProfiles JP
            INNER JOIN Users U ON JP.UserId = U.Id
            WHERE JP.UserId = @UserId
        ";

        JobSeekerProfileDto? jobSeekerDto = await connection.QueryFirstOrDefaultAsync<JobSeekerProfileDto>(sql, new { UserId = userId.Value });

        if (jobSeekerDto is null)
        {
            // do something
            return JobSeekerError.NotFound;
        }

        return jobSeekerDto;
    }
}