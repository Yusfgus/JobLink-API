using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.Jobs.DTOs;

public sealed record JobSummaryDto(
    Guid Id,
    string Title,
    JobType JobType,
    JobLocationType LocationType,
    string CompanyName,
    string? CompanyLogoUrl,
    string? Country,
    string? City,
    string Description,
    ExperienceLevel ExperienceLevel,
    List<string> Skills,
    DateTime PostedAtUtc,
    bool IsApplied,
    bool IsSaved
);
