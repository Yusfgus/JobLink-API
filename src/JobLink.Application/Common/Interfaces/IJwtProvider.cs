using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;

namespace JobLink.Application.Common.Interfaces;

public interface IJwtProvider
{
    Task<Result<TokenDto>> GenerateJWTAsync(Guid userId, string email, UserRole role, CancellationToken ct);
    Task<Result<TokenDto>> RefreshAsync(string refreshToken, CancellationToken ct);
}
