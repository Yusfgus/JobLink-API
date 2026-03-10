using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Enums;
using JobLink.Domain.JobSeekers;

namespace JobLink.Domain.JobApplications;

public sealed class JobApplication : Entity
{
    public Guid JobSeekerProfileId { get; private set; }
    public Guid JobId { get; private set; }
    public ApplicationStatus Status { get; private set; }
    public DateTime AppliedAtUtc { get; private set; }

    public JobSeekerProfile? JobSeekerProfile { get; private set; }
    public Job? Job { get; private set; }

    private JobApplication() { }

    private JobApplication(Guid jobSeekerProfileId, Guid jobId, ApplicationStatus status, DateTime appliedAtUtc)
    {
        JobSeekerProfileId = jobSeekerProfileId;
        JobId = jobId;
        Status = status;
        AppliedAtUtc = appliedAtUtc;
    }

    public static Result<JobApplication> Create(Guid jobSeekerProfileId, Guid jobId, ApplicationStatus status)
    {
        List<Error> errors = [];

        if (jobSeekerProfileId == Guid.Empty)
        {
            errors.Add(JobApplicationError.JobSeekerProfileIdRequired);
        }

        if (jobId == Guid.Empty)
        {
            errors.Add(JobApplicationError.JobIdRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobApplication(jobSeekerProfileId, jobId, status, DateTime.UtcNow);
    }
}

public static class JobApplicationError
{
    public static Error JobSeekerProfileIdRequired => Error.Validation("JobApplication_JobSeekerProfileId_Required", "JobSeekerProfileId is required");
    public static Error JobIdRequired => Error.Validation("JobApplication_JobId_Required", "JobId is required");
}