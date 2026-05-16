using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.Jobs.DTOs;

public sealed record JobSummaryDto(
    Guid Id,
    string Title,
    JobType JobType,
    JobLocationType LocationType,
    Guid CompanyId,
    string CompanyName,
    string? CompanyLogoUrl,
    string? Country,
    string? City,
    string Description,
    ExperienceLevel ExperienceLevel,
    List<JobSkillDto> Skills,
    DateTime PostedAtUtc,
    bool IsApplied,
    bool IsSaved
);
