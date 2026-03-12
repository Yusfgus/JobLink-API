using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.JobSeekers;

internal static class JobSeekerError
{
    public static Error MobileNumberAlreadyExists => Error.Conflict("JobSeeker.MobileNumber.AlreadyExists", "Mobile number already exists");
    public static Error NotFound => Error.NotFound("JobSeeker.NotFound", "Job seeker not found");
}