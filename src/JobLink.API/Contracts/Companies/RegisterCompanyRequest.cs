namespace JobLink.API.Contracts.Companies;

public record RegisterCompanyRequest(
    string Email,
    string Password,
    string Name,
    string Industry
);