using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Identity;

namespace JobLink.Infrastructure.Identity;

public class JwtProvider(TimeProvider timeProvider, IConfiguration configuration, RefreshTokenRepository refreshTokenRepo) : IJwtProvider
{
    public async Task<Result<TokenDto>> GenerateJWTAsync(GenerateJWTRequest request, CancellationToken ct)
    {
        // create claims
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, request.UserId.ToString()),
            new (ClaimTypes.Email, request.Email),
            new (ClaimTypes.Role, request.Role.ToString()),
        };

        // foreach(var permission in request.Permissions)
        //     claims.Add(new("permission", permission));

        // ----------

        var expires = timeProvider.GetUtcNow().UtcDateTime.AddMinutes(AccessTokenExpirationInMinutes);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // create jwt security token
        var securityToken = new JwtSecurityToken
        (
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

        var refreshTokenResult = await GenerateRefreshTokenAsync(request.UserId, ct);

        if (refreshTokenResult.IsFailure)
        {
            return refreshTokenResult.Errors;
        }

        string refreshToken = refreshTokenResult.Value!;

        return new TokenDto(accessToken, refreshToken, expires, request.Role);
    }

    public async Task<Result<TokenDto>> RefreshAsync(string refreshToken, CancellationToken ct)
    {
        var oldRefreshToken = await refreshTokenRepo.GetByTokenAsync(refreshToken, ct);

        if (oldRefreshToken is null)
        {
            return Error.Unauthorized("REFRESH_TOKEN_INVALID", "Invalid refresh token");
        }

        if (oldRefreshToken.ExpiresOnUtc < timeProvider.GetUtcNow())
        {
            return Error.Unauthorized("REFRESH_TOKEN_EXPIRED", "Refresh token expired");
        }

        // get user info for db
        User user = oldRefreshToken.User;

        // create new token
        return await GenerateJWTAsync(new GenerateJWTRequest(user.Id, user.Email, user.Role), ct);
    }

    private Result<ClaimsPrincipal> GetClaimsPrincipal(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return Error.Unauthorized("REFRESH_TOKEN_INVALID", "Invalid refresh token");
        }

        return principal;
    }

    private async Task<Result<string>> GenerateRefreshTokenAsync(Guid userId, CancellationToken ct)
    {
        await RevokeAllAsync(userId, ct);

        string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        var refreshTokenResult = RefreshToken.Create
        (
            userId: userId,
            token: token,
            expiresOnUtc: timeProvider.GetUtcNow().UtcDateTime.AddDays(RefreshTokenExpirationInDays)
        );

        if (refreshTokenResult.IsFailure)
        {
            return refreshTokenResult.Errors;
        }

        await refreshTokenRepo.AddAsync(refreshTokenResult.Value!, ct);

        return token;
    }

    public async Task<Result> RevokeAsync(string refreshToken, CancellationToken ct)
    {
        var oldRefreshToken = await refreshTokenRepo.GetByTokenAsync(refreshToken, ct);

        if (oldRefreshToken is null)
        {
            return Error.Unauthorized("REFRESH_TOKEN_INVALID", "Invalid refresh token");
        }

        await refreshTokenRepo.RemoveAllAsync(oldRefreshToken.UserId, ct);

        return Result.Success();
    }

    public async Task<Result> RevokeAllAsync(Guid userId, CancellationToken ct)
    {
        await refreshTokenRepo.RemoveAllAsync(userId, ct);

        return Result.Success();
    }

    private IConfigurationSection JwtSettings => configuration.GetSection("JwtSettings");
    private string Issuer => JwtSettings["Issuer"]!;
    private string Audience => JwtSettings["Audience"]!;
    private string SecretKey => JwtSettings["SecretKey"]!;
    private int AccessTokenExpirationInMinutes => int.Parse(JwtSettings["AccessTokenExpirationInMinutes"]!);
    private int RefreshTokenExpirationInDays => int.Parse(JwtSettings["RefreshTokenExpirationInDays"]!);

}

