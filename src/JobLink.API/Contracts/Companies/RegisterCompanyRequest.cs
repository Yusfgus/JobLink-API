using JobLink.Application.Common.DTOs;

namespace JobLink.API.Contracts.Companies;

public record RegisterCompanyRequest(
    string Email,
    string Password,
    string? ProfilePictureUrl,
    string? Summary,
    string Name,
    string Industry,
    string? Website,
    List<AddressDto>? Locations
);