using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Skills;

namespace JobLink.Domain.Companies.Jobs;

public sealed class JobSkill : Entity
{
    public Guid JobId { get; private set; } = default!;
    public Guid SkillId { get; private set; } = default!;
    public bool IsRequired { get; private set; }

    public Job? Job { get; private set; }
    public Skill? Skill { get; private set; }

    private JobSkill() { }

    private JobSkill(Guid jobId, Guid skillId, bool isRequired)
    {
        JobId = jobId;
        SkillId = skillId;
        IsRequired = isRequired;
    }

    public static Result<JobSkill> Create(Guid jobId, Guid skillId, bool isRequired = true)
    {
        List<Error> errors = [];

        if (jobId == Guid.Empty)
        {
            errors.Add(JobSkillError.JobIdRequired);
        }

        if (skillId == Guid.Empty)
        {
            errors.Add(JobSkillError.SkillIdRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobSkill(jobId, skillId, isRequired);
    }

    public Result Update(Guid? skillId, bool? isRequired)
    {
        if (skillId.HasValue)
        {
            if (skillId.Value == Guid.Empty)
            {
                return JobSkillError.SkillIdRequired;
            }

            SkillId = skillId.Value;
        }

        if (isRequired.HasValue)
        {
            IsRequired = isRequired.Value;
        }

        return Result.Success();
    }
}

public static class JobSkillError
{
    public static Error JobIdRequired => Error.Validation("JobSkill_JobId_Required", "JobId is required");
    public static Error SkillIdRequired => Error.Validation("JobSkill_SkillId_Required", "SkillId is required");
}