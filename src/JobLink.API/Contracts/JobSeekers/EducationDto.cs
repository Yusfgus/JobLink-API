using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record EducationDto(
    string Degree,
    string Country,
    string Institution,
    string FieldOfStudy,
    DateOnly StartDate,
    DateOnly EndDate,
    AcademicGrade Grade
);
