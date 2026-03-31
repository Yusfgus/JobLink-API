namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record ExperienceDto(
    string Id,
    string Company,
    string Position,
    string Country,
    string? Description,
    Int64 Salary,
    string StartDate,
    string EndDate
);
