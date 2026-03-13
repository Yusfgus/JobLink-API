namespace JobLink.Application.Common.DTOs;

public sealed record CompanyProfileDto(
    string Id,
    string Name,
    string Email,
    string? Summary
);
