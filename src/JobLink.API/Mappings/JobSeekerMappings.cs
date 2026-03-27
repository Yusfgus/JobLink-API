using JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;
using JobLink.API.Contracts.JobSeekers;
using JobLink.Application.Common.DTOs;
using JobLink.Application.Features.JobSeekers.Skills.Commands.AddJobSeekerSkill;
using JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;
using JobLink.Application.Features.JobSeekers.Educations.Commands.AddEducation;
using JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;
using JobLink.Application.Features.JobSeekers.Experiences.Commands.AddExperience;
using JobLink.Application.Features.JobSeekers.Experiences.Commands.UpdateExperience;

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

    public static AddJobSeekerSkillCommand ToCommand(this JobSeekerSkillDto request)
    {
        return new AddJobSeekerSkillCommand(
            request.SkillId,
            request.Level
        );
    }

    public static UpdateJobSeekerSkillCommand ToCommand(this JobSeekerSkillDto request, Guid jobSeekerSkillId)
    {
        return new UpdateJobSeekerSkillCommand(
            jobSeekerSkillId,
            request.SkillId,
            request.Level
        );
    }

    public static AddEducationCommand ToCommand(this EducationDto request)
    {
        return new AddEducationCommand(
            request.Degree,
            request.Country,
            request.Institution,
            request.FieldOfStudy,
            request.StartDate,
            request.EndDate,
            request.Grade
        );
    }

    public static UpdateEducationCommand ToCommand(this EducationDto request, Guid educationId)
    {
        return new UpdateEducationCommand(
            educationId,
            request.Degree,
            request.Country,
            request.Institution,
            request.FieldOfStudy,
            request.StartDate,
            request.EndDate,
            request.Grade
        );
    }

    public static AddExperienceCommand ToCommand(this ExperienceDto request)
    {
        return new AddExperienceCommand(
            request.Company,
            request.Position,
            request.Country,
            request.Description,
            request.Salary,
            request.StartDate,
            request.EndDate
        );
    }

    public static UpdateExperienceCommand ToCommand(this ExperienceDto request, Guid experienceId)
    {
        return new UpdateExperienceCommand(
            experienceId,
            request.Company,
            request.Position,
            request.Country,
            request.Description,
            request.Salary,
            request.StartDate,
            request.EndDate
        );
    }
}