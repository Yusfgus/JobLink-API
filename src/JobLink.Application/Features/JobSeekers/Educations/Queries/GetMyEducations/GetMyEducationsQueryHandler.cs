using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.JobSeekers.Educations.Queries.GetMyEducations;

public class GetMyEducationsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyEducationsQuery, Result<IEnumerable<EducationDto>>>
{
    public async Task<Result<IEnumerable<EducationDto>>> Handle(GetMyEducationsQuery request, CancellationToken ct)
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
                E.Id,
                E.Degree,
                E.Country,
                E.Institution,
                E.FieldOfStudy,
                E.StartDate,
                E.EndDate,
                E.Grade
            FROM Educations E
            INNER JOIN JobSeekerProfiles JSP ON E.JobSeekerProfileId = JSP.Id
            WHERE JSP.UserId = @UserId
        ";

        IEnumerable<EducationDto>? jobSeekerEducationsDto = await connection.QueryAsync<EducationDto>(sql, new { UserId = userId.Value });

        if (jobSeekerEducationsDto is null)
        {
            // do something
            return JobSeekerError.NotFound;
        }

        return jobSeekerEducationsDto.ToList();
    }
}
