using JobLink.Domain.Common.Results;

namespace JobLink.Domain.ValueObjects;

public record Address
{
    public string Country { get; } = default!;
    public string City { get; } = default!;
    public string? Area { get; }

    private Address() { }

    private Address(string country, string city, string? area)
    {
        Country = country;
        City = city;
        Area = area;
    }

    public static Result<Address> Create(string country, string city, string? area)
    {
        List<Error> errors = [];

        if (string.IsNullOrEmpty(country))
        {
            errors.Add(AddressError.CountryRequired);
        }

        if (string.IsNullOrEmpty(city))
        {
            errors.Add(AddressError.CityRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Address(country, city, area);
    }
}

public static class AddressError
{
    public static Error CountryRequired => Error.Validation("Address_Country", "Country is required");
    public static Error CityRequired => Error.Validation("Address_City", "City is required");
}
