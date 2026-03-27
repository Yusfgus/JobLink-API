namespace JobLink.API.Contracts.JobSeekers;

public sealed record ExperienceDto(
    string Company,
    string Position,
    string Country,
    string? Description,
    int Salary,
    DateOnly StartDate,
    DateOnly EndDate
);
