using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.DTOs;
using Dapper;
using System.Data;

namespace JobLink.Application.Features.JobSeekers.Queries.GetJobSeekerById;

public class GetJobSeekerByIdHandler(ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetJobSeekerByIdQuery, Result<JobSeekerDto>>
{
    public async Task<Result<JobSeekerDto>> Handle(GetJobSeekerByIdQuery request, CancellationToken ct)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        string sql = @"
            SELECT JP.Id as Id, JP.FirstName, JP.MiddleName, JP.LastName, U.Email, JP.MobileNumber, JP.BirthDate, JP.Country, JP.City, JP.Area
            FROM JobSeekerProfiles JP
            INNER JOIN Users U ON JP.UserId = U.Id
            WHERE JP.Id = @Id
        ";

        JobSeekerDto? jobSeekerDto = await connection.QueryFirstOrDefaultAsync<JobSeekerDto>(sql, new { Id = request.Id });

        if (jobSeekerDto is null)
        {
            return JobSeekerError.NotFound;
        }

        return jobSeekerDto;
    }
}