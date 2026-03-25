using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.Admin;

public record RegisterAdminRequest(
    string Email,
    string Password,
    Gender Gender
);