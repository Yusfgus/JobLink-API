namespace JobLink.Domain.ValueObjects;

public record Address(
    string Country,
    string City,
    string Area
);
