namespace JobLink.Domain.ValueObjects;

public record FullName(
    string FirstName,
    string? MiddleName,
    string LastName
);
