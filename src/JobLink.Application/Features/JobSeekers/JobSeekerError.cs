using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.JobSeekers;

internal static class JobSeekerError
{
    public static Error MobileNumberAlreadyExists => Error.Conflict("JobSeeker_MobileNumber_AlreadyExists", "Mobile number already exists");
}