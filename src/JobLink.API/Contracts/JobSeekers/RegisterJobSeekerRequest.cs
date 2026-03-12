using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public record RegisterJobSeekerRequest(
    string Email,
    string Password,
    string? ProfilePictureUrl,
    string? Summary,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? MobileNumber,
    DateOnly? BirthDate,
    string? Country,
    string? City,
    string? Area,
    Gender Gender,
    string? Nationality,
    MilitaryStatus? MilitaryStatus,
    MaritalStatus? MaritalStatus
);