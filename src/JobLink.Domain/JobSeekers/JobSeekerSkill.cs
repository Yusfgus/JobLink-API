using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Skills;

namespace JobLink.Domain.JobSeekers;

public sealed class JobSeekerSkill : Entity
{
    public Guid JobSeekerProfileId { get; private set; }
    public Guid SkillId { get; private set; }
    public SkillLevel SkillLevel { get; private set; }

    public JobSeekerProfile? JobSeekerProfile { get; private set; }
    public Skill? Skill { get; private set; }

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