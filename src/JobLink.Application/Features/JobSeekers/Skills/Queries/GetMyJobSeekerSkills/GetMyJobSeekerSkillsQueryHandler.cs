using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.JobSeekers.Skills.Queries.GetMyJobSeekerSkills;

public class GetMyJobSeekerSkillsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyJobSeekerSkillsQuery, Result<IEnumerable<JobSeekerSkillDto>>>
{
    public async Task<Result<IEnumerable<JobSeekerSkillDto>>> Handle(GetMyJobSeekerSkillsQuery request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;

        if (userId is null)
        {
            // do something
            return IdentityError.UserNotFound;
        }

        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        Guid? jobSeekerProfileId = appUser.JobSeekerId;

        if (jobSeekerProfileId is null)
        {
            // do something
            return JobSeekerError.NotFound;
        }

        const string sql = @"
            SELECT 
                JS.Id,
                S.Id as SkillId,
                S.Name,
                JS.SkillLevel
            FROM JobSeekerSkills JS
            INNER JOIN Skills S ON JS.SkillId = S.Id
            WHERE JS.JobSeekerProfileId = @JobSeekerProfileId
        ";

        IEnumerable<JobSeekerSkillDto>? jobSeekerSkillsDto = await connection.QueryAsync<JobSeekerSkillDto>(sql, new { JobSeekerProfileId = jobSeekerProfileId! });

        if (jobSeekerSkillsDto is null)
        {
            // do something
            return JobSeekerError.NotFound;
        }

        return jobSeekerSkillsDto.ToList();
    }
}
