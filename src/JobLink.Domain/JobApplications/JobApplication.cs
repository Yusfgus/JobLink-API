using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Enums;
using JobLink.Domain.JobSeekers;

namespace JobLink.Domain.JobApplications;

public sealed class JobApplication : Entity
{
    public Guid JobSeekerId { get; }
    public Guid JobId { get; }
    public ApplicationStatus Status { get; }
    public DateTime AppliedAtUtc { get; }

    public JobSeekerProfile? JobSeekerProfile { get; }
    public Job? Job { get; }

    private JobApplication() { }

    private JobApplication(Guid jobSeekerId, Guid jobId, ApplicationStatus status, DateTime appliedAtUtc)
    {
        JobSeekerId = jobSeekerId;
        JobId = jobId;
        Status = status;
        AppliedAtUtc = appliedAtUtc;
    }

    public static Result<JobApplication> Create(Guid jobSeekerId, Guid jobId, ApplicationStatus status)
    {
        List<Error> errors = [];

        if (jobSeekerId == Guid.Empty)
        {
            errors.Add(JobApplicationError.JobSeekerIdRequired);
        }

        if (jobId == Guid.Empty)
        {
            errors.Add(JobApplicationError.JobIdRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobApplication(jobSeekerId, jobId, status, DateTime.UtcNow);
    }
}

public static class JobApplicationError
{
    public static Error JobSeekerIdRequired => Error.Validation("JobApplication_JobSeekerId_Required", "JobSeekerId is required");
    public static Error JobIdRequired => Error.Validation("JobApplication_JobId_Required", "JobId is required");
}