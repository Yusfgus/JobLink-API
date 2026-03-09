using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.JobSeekers;

namespace JobLink.Domain.SavedJobs;

public sealed class SavedJob : Entity
{
    public Guid JobSeekerId { get; } = default!;
    public Guid JobId { get; } = default!;
    public DateTime SavedAtUtc { get; } = default!;

    public JobSeekerProfile? JobSeekerProfile { get; }
    public Job? Job { get; }

    private SavedJob() { }

    private SavedJob(Guid jobSeekerId, Guid jobId, DateTime savedAtUtc)
    {
        JobSeekerId = jobSeekerId;
        JobId = jobId;
        SavedAtUtc = savedAtUtc;
    }

    public static Result<SavedJob> Create(Guid jobSeekerId, Guid jobId)
    {
        List<Error> errors = [];

        if (jobSeekerId == Guid.Empty)
        {
            errors.Add(SavedJobError.JobSeekerIdRequired);
        }

        if (jobId == Guid.Empty)
        {
            errors.Add(SavedJobError.JobIdRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new SavedJob(jobSeekerId, jobId, DateTime.UtcNow);
    }
}

public static class SavedJobError
{
    public static Error JobSeekerIdRequired => Error.Validation("SavedJob_JobSeekerId_Required", "JobSeekerId is required");
    public static Error JobIdRequired => Error.Validation("SavedJob_JobId_Required", "JobId is required");
}