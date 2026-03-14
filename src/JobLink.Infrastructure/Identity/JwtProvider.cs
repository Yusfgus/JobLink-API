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
using JobLink.Domain.Common.Enums;

namespace JobLink.Infrastructure.Identity;

public class JwtProvider(TimeProvider timeProvider, IConfiguration configuration, RefreshTokenRepository refreshTokenRepo) : IJwtProvider
{
    public async Task<Result<TokenDto>> GenerateJWTAsync(Guid userId, string email, UserRole role, CancellationToken ct)
    {
        // create claims
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, userId.ToString()),
            new (ClaimTypes.Email, email),
            new (ClaimTypes.Role, role.ToString()),
        };

        // foreach(var permission in request.Permissions)
        //     claims.Add(new("permission", permission));

        // ----------

        // get jwtSettings from the configurations
        var jwtSettings = configuration.GetSection("JwtSettings");

        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;
        var secretKey = jwtSettings["SecretKey"]!;
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

        // ----------

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // create jwt security token
        var securityToken = new JwtSecurityToken
        (
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

        var refreshTokenResult = await GenerateRefreshTokenAsync(userId, ct);

        if (refreshTokenResult.IsFailure)
        {
            return refreshTokenResult.Errors;
        }

        string refreshToken = refreshTokenResult.Value!;

        return new TokenDto(accessToken, refreshToken, expires);
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

        User user = oldRefreshToken.User;

        // create new token
        return await GenerateJWTAsync(user.Id, user.Email, user.Role, ct);
    }

    private async Task<Result<string>> GenerateRefreshTokenAsync(Guid userId, CancellationToken ct)
    {
        await refreshTokenRepo.RemoveAllAsync(userId, ct);

        string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        var refreshTokenResult = RefreshToken.Create
        (
            userId: userId,
            token: token,
            expiresOnUtc: DateTime.UtcNow.AddDays(7)
        );

        if (refreshTokenResult.IsFailure)
        {
            return refreshTokenResult.Errors;
        }

        await refreshTokenRepo.AddAsync(refreshTokenResult.Value!, ct);

        return token;
    }

}

