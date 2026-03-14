using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;

namespace JobLink.Application.Common.Interfaces;

public interface IUserService
{
    Task<Result<Guid>> RegisterUser(RegisterUserDto registerUserDto, UserRole role, CancellationToken ct);
}
