using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.JobSeekers.Experiences.Queries.GetMyExperiences;

public class GetMyExperiencesQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyExperiencesQuery, Result<IEnumerable<ExperienceDto>>>
{
    public async Task<Result<IEnumerable<ExperienceDto>>> Handle(GetMyExperiencesQuery request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;
        if (userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT 
                E.Id,
                E.Company,
                E.Position,
                E.Country,
                E.Description,
                E.Salary,
                E.StartDate,
                E.EndDate
            FROM Experiences E
            INNER JOIN JobSeekerProfiles JSP ON E.JobSeekerProfileId = JSP.Id
            WHERE JSP.UserId = @UserId
        ";

        IEnumerable<ExperienceDto>? jobSeekerExperiencesDto = await connection.QueryAsync<ExperienceDto>(sql, new { UserId = userId.Value });

        if (jobSeekerExperiencesDto is null)
        {
            return JobSeekerError.NotFound;
        }

        return jobSeekerExperiencesDto.ToList();
    }
}
