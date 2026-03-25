using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Features.JobSeekers.DTOs;
using Dapper;
using System.Data;
using JobLink.Application.Features.Identity;

namespace JobLink.Application.Features.JobSeekers.Skills.Queries.GetMyJobSeekerSkillById;

public class GetMyJobSeekerSkillByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IAppUser appUser) : IRequestHandler<GetMyJobSeekerSkillByIdQuery, Result<JobSeekerSkillDto>>
{
    public async Task<Result<JobSeekerSkillDto>> Handle(GetMyJobSeekerSkillByIdQuery request, CancellationToken ct)
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
            INNER JOIN JobSeekerProfiles JSP ON JS.JobSeekerProfileId = JSP.Id
            INNER JOIN Skills S ON JS.SkillId = S.Id
            WHERE JS.JobSeekerProfileId = @JobSeekerProfileId AND JS.Id = @JobSeekerSkillId
        ";

        JobSeekerSkillDto? jobSeekerSkillDto = await connection.QueryFirstOrDefaultAsync<JobSeekerSkillDto>(sql, new { JobSeekerProfileId = jobSeekerProfileId!, JobSeekerSkillId = request.JobSeekerSkillId });

        if (jobSeekerSkillDto is null)
        {
            // do something
            return Error.NotFound("JobSeekerSkill_NotFound", "JobSeekerSkill not found");
        }

        return jobSeekerSkillDto;
    }
}
