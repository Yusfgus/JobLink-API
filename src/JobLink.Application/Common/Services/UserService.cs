using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Common.Services;

public sealed class UserService(IAppDbContext dbContext) : IUserService
{
    public async Task<Result<Guid>> RegisterUser(RegisterUserDto registerUserDto, UserRole role, CancellationToken ct)
    {
        string email = registerUserDto.Email.Trim();

        bool exists = await dbContext.Users.AnyAsync(x => x.Email == email, ct);

        if (exists)
        {
            return Error.Conflict("User.Email.Exists", "Email already exists.");
        }

        string passwordHash = registerUserDto.Password; // TODO: Hash password

        var result = User.Create(email, passwordHash, null, role, null);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        User user = result.Value!;

        await dbContext.Users.AddAsync(user, ct);

        // await dbContext.SaveChangesAsync(ct);

        return user.Id;
    }
}