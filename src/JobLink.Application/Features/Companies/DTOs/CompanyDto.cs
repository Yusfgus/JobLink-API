using JobLink.Application.Common.DTOs;

namespace JobLink.Application.Features.Companies.DTOs;

public sealed record CompanyProfileDto(
    string Id,
    string Name,
    string Email,
    string? WebsiteUrl,
    string? Description,
    string? LogoUrl,
    List<AddressDto> Locations
);
