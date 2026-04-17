using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.JobSeekers.SavedJobs;

public static class SavedJobError
{
    public static readonly Error AlreadySaved = Error.Conflict("SavedJob.AlreadySaved", "User has already saved this job.");
    public static readonly Error NotFound = Error.NotFound("SavedJob.NotFound", "Saved job not found.");
}
