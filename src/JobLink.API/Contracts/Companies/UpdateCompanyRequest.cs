using JobLink.Application.Features.Companies.Profile.Commands.UpdateMyCompany;

namespace JobLink.API.Contracts.Companies;

public sealed record UpdateCompanyRequest(
    string? Name,
    string? Industry,
    string? Description,
    string? LogoUrl,
    string? Website
)
{
    public UpdateMyCompanyCommand ToCommand()
    {
        return new UpdateMyCompanyCommand(
            Name: Name,
            Industry: Industry,
            Description: Description,
            LogoUrl: LogoUrl,
            Website: Website
        );
    }
}