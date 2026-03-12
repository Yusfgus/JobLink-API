namespace JobLink.Application.Common.DTOs;

public sealed record JobSeekerDto(
    string Id,
    string FirstName,
    string? MiddleName,
    string LastName,
    string Email,
    string? MobileNumber,
    string? BirthDate,
    string? Country,
    string? City,
    string? Area
);
