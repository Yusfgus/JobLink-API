using JobLink.API.Contracts.JobSeekers;
using JobLink.Application.Common.DTOs;
using JobLink.Application.Features.Admins.Commands.RegisterAdmin;

namespace JobLink.API.Mappings;

public static class AdminMappings
{
    public static RegisterAdminCommand ToCommand(this RegisterAdminRequest request)
    {
        return new RegisterAdminCommand(
            new RegisterUserDto(
                request.Email,
                request.Password
            ),
            request.Gender
        );
    }
}