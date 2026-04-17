namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record SavedJobDto(
    Guid Id,
    Guid JobId,
    string JobTitle,
    Guid CompanyId,
    string CompanyName,
    string? CompanyLogoUrl,
    string Location,
    DateTime SavedAtUtc,
    bool IsApplied
);
