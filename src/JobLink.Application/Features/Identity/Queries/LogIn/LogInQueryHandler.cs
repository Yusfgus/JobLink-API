using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
// using Dapper;
using JobLink.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Identity.Queries.LogIn;

public sealed class LogInQueryHandler(IAppDbContext dbContext, IJwtProvider jwtProvider) : IRequestHandler<LogInQuery, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(LogInQuery request, CancellationToken ct)
    {
        // using var connection = sqlConnectionFactory.CreateConnection();

        // var user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition("SELECT * FROM Users WHERE Email = @Email", new { request.Email }, cancellationToken: ct));

        User? user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email, ct);

        if (user is null)
        {
            return IdentityError.InvalidCredentials;
        }

        // if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        if (user.PasswordHash != request.Password)
        {
            return IdentityError.InvalidCredentials;
        }

        var result = await jwtProvider.GenerateJWTAsync(new GenerateJWTRequest(user.Id, user.Email, user.Role), ct);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        return result.Value!;
    }
}   