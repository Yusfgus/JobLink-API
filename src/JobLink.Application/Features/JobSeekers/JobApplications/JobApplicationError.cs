using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.JobSeekers.JobApplications;

public static class JobApplicationError
{
    public static readonly Error AlreadyApplied = Error.Conflict("JobApplication.AlreadyApplied", "User has already applied for this job.");
}