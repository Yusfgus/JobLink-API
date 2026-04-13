using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;

namespace JobLink.Domain.JobSeekers;

public sealed class Resume : Entity
{
    public Guid JobSeekerProfileId { get; private set; }
    public string FileUrl { get; private set; } = default!;

    public JobSeekerProfile? JobSeekerProfile { get; private set; }

    private Resume() { }

    private Resume(Guid jobSeekerId)
    {
        JobSeekerProfileId = jobSeekerId;
    }

    public static Result<Resume> Create(Guid jobSeekerId)
    {
        List<Error> errors = [];

        if (jobSeekerId == Guid.Empty)
            errors.Add(ResumeError.JobSeekerProfileIdRequired);

        if (errors.Count > 0)
            return errors;

        return new Resume(jobSeekerId);
    }

    public Result SetFileUrl(string fileUrl)
    {
        List<Error> errors = [];

        if (string.IsNullOrEmpty(fileUrl))
            errors.Add(ResumeError.FileUrlRequired);

        if (errors.Count > 0)
            return errors;

        FileUrl = fileUrl;

        return Result.Success();
    }
}

public static class ResumeError
{
    public static Error JobSeekerProfileIdRequired => Error.Validation("Resume_JobSeekerProfileId_Required", "JobSeekerProfileId is required");
    public static Error FileUrlRequired => Error.Validation("Resume_FileUrl_Required", "FileUrl is required");
}