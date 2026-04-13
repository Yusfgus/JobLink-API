using JobLink.Domain.Common.Results;

namespace JobLink.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<Result<string>> UploadAsync(Stream fileStream, string fileName, CancellationToken ct);
    Task<Result> DeleteAsync(string filePath, CancellationToken ct);
    Task<Result<Stream>> GetAsync(string filePath, CancellationToken ct);
}