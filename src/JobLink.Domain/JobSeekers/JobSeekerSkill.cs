using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Enums;
using JobLink.Domain.Skills;

namespace JobLink.Domain.JobSeekers;

public sealed class JobSeekerSkill : Entity
{
    public Guid JobSeekerProfileId { get; }
    public Guid SkillId { get; }
    public SkillLevel SkillLevel { get; }

    public JobSeekerProfile? JobSeekerProfile { get; }
    public Skill? Skill { get; }

    private JobSeekerSkill() { }

    private JobSeekerSkill(Guid jobSeekerProfileId, Guid skillId, SkillLevel skillLevel)
    {
        JobSeekerProfileId = jobSeekerProfileId;
        SkillId = skillId;
        SkillLevel = skillLevel;
    }

    public static Result<JobSeekerSkill> Create(Guid jobSeekerProfileId, Guid skillId, SkillLevel skillLevel)
    {
        List<Error> errors = [];

        if (jobSeekerProfileId == Guid.Empty)
        {
            errors.Add(JobSeekerSkillError.JobSeekerProfileIdRequired);
        }

        if (skillId == Guid.Empty)
        {
            errors.Add(JobSeekerSkillError.SkillIdRequired);
        }

        if (skillLevel == default)
        {
            errors.Add(JobSeekerSkillError.SkillLevelRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobSeekerSkill(jobSeekerProfileId, skillId, skillLevel);
    }

}
public static class JobSeekerSkillError
{
    public static Error JobSeekerProfileIdRequired => Error.Validation("JobSeekerSkill_JobSeekerProfileId_Required", "JobSeekerProfileId is required");
    public static Error SkillIdRequired => Error.Validation("JobSeekerSkill_SkillId_Required", "SkillId is required");
    public static Error SkillLevelRequired => Error.Validation("JobSeekerSkill_SkillLevel_Required", "SkillLevel is required");
}