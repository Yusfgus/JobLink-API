using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;

namespace JobLink.Application.Features.JobSeekers.Queries.GetJobSeekerById;

public class GetJobSeekerByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetJobSeekerByIdQuery, Result<JobSeekerProfileDto>>
{
    public async Task<Result<JobSeekerProfileDto>> Handle(GetJobSeekerByIdQuery request, CancellationToken ct)
    {
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
            WHERE JP.Id = @Id
        ";

        JobSeekerProfileDto? jobSeekerDto = await connection.QueryFirstOrDefaultAsync<JobSeekerProfileDto>(sql, new { request.Id });

        if (jobSeekerDto is null)
        {
            // do something
            return JobSeekerError.NotFound;
        }

        return jobSeekerDto;
    }
}