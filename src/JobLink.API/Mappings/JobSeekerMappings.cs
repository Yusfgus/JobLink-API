using JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;
using JobLink.API.Contracts.JobSeekers;
using JobLink.Application.Common.DTOs;
using JobLink.Application.Features.JobSeekers.Skills.Commands.AddJobSeekerSkill;
using JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;

namespace JobLink.API.Mappings;

public static class JobSeekerMappings
{
    public static RegisterJobSeekerCommand ToCommand(this RegisterJobSeekerRequest request)
    {
        return new RegisterJobSeekerCommand(
            new RegisterUserDto(
                request.Email,
                request.Password
            ),
            request.FirstName,
            request.LastName,
            request.Gender
        );
    }

    public static AddJobSeekerSkillCommand ToCommand(this AddJobSeekerSkillRequest request)
    {
        return new AddJobSeekerSkillCommand(
            request.SkillId,
            request.Level
        );
    }

    public static UpdateJobSeekerSkillCommand ToCommand(this UpdateJobSeekerSkillRequest request, Guid jobSeekerSkillId)
    {
        return new UpdateJobSeekerSkillCommand(
            jobSeekerSkillId,
            request.SkillId,
            request.Level
        );
    }
}