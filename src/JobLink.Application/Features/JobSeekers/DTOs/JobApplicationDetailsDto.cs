using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record JobApplicationDetailsDto(
    Guid JobId,
    string JobTitle,
    Guid CompanyId,
    string CompanyName,
    string? Country,
    string? City,
    ApplicationStatus Status,
    DateTime AppliedAtUtc
);