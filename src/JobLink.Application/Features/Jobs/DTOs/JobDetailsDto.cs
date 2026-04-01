using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.Jobs.DTOs;

public sealed record JobDetailsDto(
    Guid Id,
    string Title,
    JobType JobType,
    JobLocationType LocationType,
    string CompanyName,
    string? CompanyLogoUrl,
    string Country,
    string City,
    string? Area,
    DateTime PostedAtUtc,
    DateOnly? ClosedAt,
    DateOnly ExpirationDate,
    JobStatus Status,
    ExperienceLevel ExperienceLevel,
    int MinSalary,
    int MaxSalary,
    List<string> Skills,
    string Description,
    string? Requirements
);