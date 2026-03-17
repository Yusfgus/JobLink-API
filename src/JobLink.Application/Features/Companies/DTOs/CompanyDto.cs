namespace JobLink.Application.Features.Companies.DTOs;

public sealed record CompanyProfileDto(
    string Id,
    string Name,
    string Email,
    string? Summary
);
