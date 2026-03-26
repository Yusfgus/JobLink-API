using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.JobSeekers.Educations.Queries.GetMyEducationById;

public class GetMyEducationByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyEducationByIdQuery, Result<EducationDto>>
{
    public async Task<Result<EducationDto>> Handle(GetMyEducationByIdQuery request, CancellationToken ct)
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
            WHERE JSP.UserId = @UserId AND E.Id = @Id
        ";

        EducationDto? jobSeekerEducationDto = await connection.QueryFirstOrDefaultAsync<EducationDto>(sql, new { UserId = userId.Value, Id = request.Id });

        if (jobSeekerEducationDto is null)
        {
            // do something
            return Error.NotFound("Education_NotFound", "Education not found");
        }

        return jobSeekerEducationDto;
    }
}
