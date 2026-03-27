using JobLink.Application.Features.Admins.Commands.RegisterAdmin;
using JobLink.Application.Common.DTOs;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.Admin;

public record RegisterAdminRequest(
    string Email,
    string Password,
    Gender Gender
)
{
    public RegisterAdminCommand ToCommand()
    {
        return new RegisterAdminCommand(
            new RegisterUserDto(
                Email,
                Password
            ),
            Gender
        );
    }
}