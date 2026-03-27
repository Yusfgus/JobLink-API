using JobLink.Application.Features.Companies.Commands.RegisterCompany;
using JobLink.Application.Common.DTOs;

namespace JobLink.API.Contracts.Companies;

public record RegisterCompanyRequest(
    string Email,
    string Password,
    string Name,
    string Industry
)
{
    public RegisterCompanyCommand ToCommand()
    {
        return new RegisterCompanyCommand(
            new RegisterUserDto(
                Email,
                Password
            ),
            Name,
            Industry
        );
    }
}