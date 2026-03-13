namespace JobLink.Application.Common.DTOs;

public sealed record JobSeekerProfileDto(
    string Id,
    string FirstName,
    string? MiddleName,
    string LastName,
    string Email,
    string? MobileNumber,
    string? BirthDate,
    string? Gender,
    string? Nationality,
    string? MilitaryStatus,
    string? MaritalStatus,
    string? Country,
    string? City,
    string? Area
);
