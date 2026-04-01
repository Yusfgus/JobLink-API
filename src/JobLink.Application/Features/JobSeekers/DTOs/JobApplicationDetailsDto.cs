using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record JobApplicationDetailsDto(
    Guid Id,
    Guid JobId,
    string JobTitle,
    Guid CompanyId,
    string CompanyName,
    string Location,
    ApplicationStatus Status,
    DateTime AppliedAtUtc
);