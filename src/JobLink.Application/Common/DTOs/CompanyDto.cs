namespace JobLink.Application.Common.DTOs;

public sealed record CompanyDto(
    string Id,
    string Name,
    string Email,
    string? Summary
);
