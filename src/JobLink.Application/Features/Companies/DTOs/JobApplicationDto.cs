using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.Companies.DTOs;

public sealed record JobApplicantsDto(
    Guid Id,
    Guid JobSeekerProfileId,
    string JobSeekerName,
    string JobSeekerEmail,
    string? JobSeekerMobileNumber,
    string? JobSeekerProfilePictureUrl,
    string? JobSeekerResumeUrl,
    DateTime AppliedAtUtc,
    ApplicationStatus Status
);
