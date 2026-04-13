using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Resumes.Commands.UploadMyResume;

public class UploadResumeCommandHandler(IAppDbContext dbContext, IAppUser appUser, IFileStorageService fileStorageService): IRequestHandler<UploadMyResumeCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UploadMyResumeCommand request, CancellationToken ct)
    {
        var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
        var extension = Path.GetExtension(request.FileName);

        if (!allowedExtensions.Contains(extension))
        {
            return Error.Validation("Resume_InvalidFileType", "Invalid file type");
        }

        if (request.FileStream.Length > 2 * 1024 * 1024)
        {
            return Error.Validation("Resume_FileTooLarge", "File too large");
        }

        JobSeekerProfile? jobSeekerProfile = await dbContext.JobSeekerProfiles
            .Include(j => j.Resume)
            .FirstOrDefaultAsync(j => j.UserId == appUser.UserId, ct);

        if (jobSeekerProfile is null)
        {
            return JobSeekerError.NotFound;
        }

        if (jobSeekerProfile.Resume is not null)
        {
            return Error.Conflict("Resume_AlreadyExists", "Resume already exists");
        }

        var resumeResult = Resume.Create(jobSeekerProfile.Id);
        if (resumeResult.IsFailure)
        {
            return resumeResult.Errors;
        }

        Resume resume = resumeResult.Value!;
        string fileName = $"{jobSeekerProfile.Id}{extension}";

        var filePathResult = await fileStorageService.UploadAsync(request.FileStream, fileName, ct);
        if (filePathResult.IsFailure)
        {
            return filePathResult.Errors;
        }

        var fileUrlResult = resume.SetFileUrl(filePathResult.Value!);
        if (fileUrlResult.IsFailure)
        {
            return fileUrlResult.Errors;
        }

        dbContext.Resumes.Add(resume);
        await dbContext.SaveChangesAsync(ct);

        return resume.Id;
    }
}

