using JobLink.Domain.Common.Results;

namespace JobLink.Domain.Common.ValueObjects;

public record Address
{
    public string? Country { get; }
    public string? City { get; }
    public string? Area { get; }

    private Address(string? country, string? city, string? area)
    {
        Country = country;
        City = city;
        Area = area;
    }

    public static Result<Address> Create(string? country, string? city, string? area)
    {
        // Address components are optional in this domain, but we keep the structure.
        return new Address(country, city, area);
    }
}