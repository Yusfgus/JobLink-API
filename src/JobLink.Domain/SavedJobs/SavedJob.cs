using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.JobSeekers;

namespace JobLink.Domain.SavedJobs;

public sealed class SavedJob : Entity
{
    public Guid JobSeekerProfileId { get; private set; } = default!;
    public Guid JobId { get; private set; } = default!;
    public DateTime SavedAtUtc { get; private set; } = default!;

    public JobSeekerProfile? JobSeekerProfile { get; private set; }
    public Job? Job { get; private set; }

    private SavedJob() { }

    private SavedJob(Guid jobSeekerProfileId, Guid jobId, DateTime savedAtUtc)
    {
        JobSeekerProfileId = jobSeekerProfileId;
        JobId = jobId;
        SavedAtUtc = savedAtUtc;
    }

    public static Result<SavedJob> Create(Guid jobSeekerProfileId, Guid jobId)
    {
        List<Error> errors = [];

        if (jobSeekerProfileId == Guid.Empty)
        {
            errors.Add(SavedJobError.JobSeekerProfileIdRequired);
        }

        if (jobId == Guid.Empty)
        {
            errors.Add(SavedJobError.JobIdRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new SavedJob(jobSeekerProfileId, jobId, DateTime.UtcNow);
    }
}

public static class SavedJobError
{
    public static Error JobSeekerProfileIdRequired => Error.Validation("SavedJob_JobSeekerProfileId_Required", "JobSeekerProfileId is required");
    public static Error JobIdRequired => Error.Validation("SavedJob_JobId_Required", "JobId is required");
}