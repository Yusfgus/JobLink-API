using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public record RegisterAdminRequest(
    string Email,
    string Password,
    Gender Gender
);