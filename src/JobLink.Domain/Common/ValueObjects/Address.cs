namespace JobLink.Domain.Common.ValueObjects;

public class Address
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Area { get; set; }

    public Address(string? country, string? city, string? area)
    {
        Country = country;
        City = city;
        Area = area;
    }
}