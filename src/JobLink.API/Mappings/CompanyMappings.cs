using JobLink.Application.Features.Companies.Commands.RegisterCompany;
using JobLink.API.Contracts.Companies;
using JobLink.Application.Common.DTOs;

namespace JobLink.API.Mappings;

public static class CompanyMappings
{
    public static RegisterCompanyCommand ToCommand(this RegisterCompanyRequest request)
    {
        return new RegisterCompanyCommand(
            new RegisterUserDto(
                request.Email,
                request.Password
            ),
            request.Name,
            request.Industry
        );
    }
}