using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record RegisterJobSeekerRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    Gender Gender
);
