using JobLink.Application.Features.Jobs.DTOs;
using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record SavedJobDto(
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
    DateTime SavedAtUtc
);
