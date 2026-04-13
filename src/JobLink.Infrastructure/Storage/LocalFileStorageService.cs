using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;

namespace JobLink.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    public async Task<Result<string>> UploadAsync(Stream fileStream, string fileName, CancellationToken ct)
    {
        var path = Path.Combine("Storage/Resumes", fileName);

        using var fileStreamOutput = new FileStream(path, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput, ct);

        return path;
    }

    public Task<Result> DeleteAsync(string filePath, CancellationToken ct)
    {
        if (!File.Exists(filePath))
            return Task.FromResult(Result.Failure(Error.NotFound("File not found")));

        File.Delete(filePath);

        return Task.FromResult(Result.Success());
    }

    public Task<Result<Stream>> GetAsync(string filePath, CancellationToken ct)
    {
        if (!File.Exists(filePath))
            return Task.FromResult(Result<Stream>.Failure(Error.NotFound("File not found")));

        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(Result<Stream>.Success(stream));
    }
}