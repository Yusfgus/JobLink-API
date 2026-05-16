using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobSeekers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.JobSeekers.Pictures.Commands.UploadMyPicture;

public class UploadPictureCommandHandler(IAppDbContext dbContext, IAppUser appUser, IFileStorageService fileStorageService): IRequestHandler<UploadMyPictureCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UploadMyPictureCommand request, CancellationToken ct)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(request.FileName);

        if (!allowedExtensions.Contains(extension))
        {
            return Error.Validation("Picture_InvalidFileType", "Invalid file type");
        }

        if (request.FileStream.Length > 5 * 1024 * 1024)
        {
            return Error.Validation("Picture_FileTooLarge", "File too large");
        }

        JobSeekerProfile? jobSeekerProfile = await dbContext.JobSeekerProfiles.FirstOrDefaultAsync(j => j.UserId == appUser.UserId, ct);
        if (jobSeekerProfile is null)
        {
            return JobSeekerError.NotFound;
        }

        string fileName = $"{Guid.NewGuid()}{extension}";

        var filePathResult = await fileStorageService.UploadPictureAsync(request.FileStream, fileName, ct);
        if (filePathResult.IsFailure)
        {
            return filePathResult.Errors;
        }

        string? oldPictureUrl = jobSeekerProfile.ProfilePictureUrl;
        var updateResult = jobSeekerProfile.SetProfilePicture(filePathResult.Value!);
        if (updateResult.IsFailure)
        {
            return updateResult.Errors;
        }

        if (!string.IsNullOrEmpty(oldPictureUrl))
        {
            await fileStorageService.DeleteAsync(oldPictureUrl, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        return filePathResult;
    }
}

