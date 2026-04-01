using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record JobApplicationSummaryDto(
    Guid Id,
    string JobTitle,
    string CompanyName,
    string? CompanyLogoUrl,
    string Location,
    ApplicationStatus Status,
    DateTime AppliedAtUtc
);
