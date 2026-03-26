namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record EducationDto(
    string Id,
    string Degree,
    string Country,
    string Institution,
    string FieldOfStudy,
    string StartDate,
    string EndDate,
    string Grade
);