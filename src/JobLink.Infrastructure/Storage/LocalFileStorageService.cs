using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using Microsoft.AspNetCore.Hosting;

namespace JobLink.Infrastructure.Storage;

public class LocalFileStorageService(IWebHostEnvironment webHostEnvironment) : IFileStorageService
{
    private readonly string _webRootPath = webHostEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

    public async Task<Result<string>> UploadResumeAsync(Stream fileStream, string fileName, CancellationToken ct)
    {
        var folderPath = Path.Combine(_webRootPath, "uploads", "resumes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fullPath = Path.Combine(folderPath, fileName);
        var relativePath = Path.Combine("uploads", "resumes", fileName).Replace("\\", "/");

        using var fileStreamOutput = new FileStream(fullPath, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput, ct);

        return relativePath;
    }

    public async Task<Result<string>> UploadPictureAsync(Stream fileStream, string fileName, CancellationToken ct)
    {
        var folderPath = Path.Combine(_webRootPath, "uploads", "pictures");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fullPath = Path.Combine(folderPath, fileName);
        var relativePath = Path.Combine("uploads", "pictures", fileName).Replace("\\", "/");

        using var fileStreamOutput = new FileStream(fullPath, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput, ct);

        return relativePath;
    }

    public Task<Result> DeleteAsync(string filePath, CancellationToken ct)
    {
        var fullPath = Path.Combine(_webRootPath, filePath);
        if (!File.Exists(fullPath))
            return Task.FromResult(Result.Failure(Error.NotFound("File not found")));

        File.Delete(fullPath);

        return Task.FromResult(Result.Success());
    }

    public Task<Result<Stream>> GetAsync(string filePath, CancellationToken ct)
    {
        var fullPath = Path.Combine(_webRootPath, filePath);
        if (!File.Exists(fullPath))
            return Task.FromResult(Result<Stream>.Failure(Error.NotFound("File not found")));

        Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(Result<Stream>.Success(stream));
    }
}