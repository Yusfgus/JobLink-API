using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record JobApplicationSummaryDto(
    Guid JobId,
    string JobTitle,
    Guid CompanyId,
    string CompanyName,
    string? CompanyLogoUrl,
    string? Country,
    string? City,
    ApplicationStatus Status,
    DateTime AppliedAtUtc
);
