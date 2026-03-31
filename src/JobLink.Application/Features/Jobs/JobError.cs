using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.Jobs;

public static class JobError
{
    public static readonly Error NotFound = Error.NotFound("Job.NotFound", "Job not found");
}