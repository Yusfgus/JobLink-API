using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record JobApplicationSummaryDto(
    Guid Id,
    Guid JobId,
    string JobTitle,
    Guid CompanyId,
    string CompanyName,
    string? CompanyLogoUrl,
    string Location,
    ApplicationStatus Status,
    DateTime AppliedAtUtc
);
