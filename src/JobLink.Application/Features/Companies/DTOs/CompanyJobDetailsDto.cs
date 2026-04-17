using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.Companies.DTOs;

public sealed record CompanyJobDetailsDto(
    Guid Id,
    string Title,
    JobType JobType,
    JobLocationType LocationType,
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
    List<CompanyJobSkillDto> Skills,
    string Description,
    string? Requirements
);