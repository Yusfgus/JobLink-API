using JobLink.Domain.Common.Enums;

namespace JobLink.Application.Features.Companies.DTOs;

public sealed record CompanyJobSummaryDto(
    Guid Id,
    string Title,
    JobType JobType,
    JobLocationType LocationType,
    string? Country,
    string? City,
    ExperienceLevel ExperienceLevel,
    List<string> Skills,
    DateTime PostedAtUtc
);