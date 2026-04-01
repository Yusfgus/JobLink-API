namespace JobLink.Application.Features.Companies.DTOs;

public sealed record CompanyJobDto(
    string Id,
    string Title,
    string Description,
    string? Requirements,
    string ExperienceLevel,
    string JobType,
    string LocationType,
    string Country,
    string City,
    string? Area,
    Int64 MinSalary,
    Int64 MaxSalary,
    string PostedAtUtc,
    string? ClosedAt,
    string ExpirationDate,
    string Status
);