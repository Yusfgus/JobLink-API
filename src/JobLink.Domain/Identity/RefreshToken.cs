using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;

namespace JobLink.Domain.Identity;

public sealed class RefreshToken : Entity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public DateTimeOffset ExpiresOnUtc { get; private set; }

    // Navigation
    public User User { get; private set; } = null!;

    private RefreshToken() { } // EF Core

    private RefreshToken(Guid userId, string token, DateTimeOffset expiresOnUtc)
    {
        UserId = userId;
        Token = token;
        ExpiresOnUtc = expiresOnUtc;
    }

    public static Result<RefreshToken> Create(Guid userId, string? token, DateTimeOffset expiresOnUtc)
    {
        if (userId == Guid.Empty)
            return RefreshTokenErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(token))
            return RefreshTokenErrors.TokenRequired;

        if (expiresOnUtc <= DateTimeOffset.UtcNow)
            return RefreshTokenErrors.ExpiryInPast;

        return new RefreshToken(userId, token.Trim(), expiresOnUtc);
    }
}

public static class RefreshTokenErrors
{
    public static Error UserIdRequired => Error.Validation("REFRESH_TOKEN_USER_ID_REQUIRED", "UserId is required for refresh token");
    public static Error TokenRequired => Error.Validation("REFRESH_TOKEN_VALUE_REQUIRED", "Refresh token value is required");
    public static Error ExpiryInPast => Error.Validation("REFRESH_TOKEN_EXPIRY_IN_PAST", "Refresh token expiry must be in the future");
}
