using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;

namespace JobLink.Application.Common.Interfaces;

public sealed record GenerateJWTRequest(Guid UserId, string Email, UserRole Role);

public interface IJwtProvider
{
    Task<Result<TokenDto>> GenerateJWTAsync(GenerateJWTRequest request, CancellationToken ct);
    Task<Result<TokenDto>> RefreshAsync(string refreshToken, CancellationToken ct);
}
