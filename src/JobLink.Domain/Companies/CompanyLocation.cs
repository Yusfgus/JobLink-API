using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.ValueObjects;

namespace JobLink.Domain.Companies;

public sealed class CompanyLocation : Entity
{
    public Guid CompanyProfileId { get; private set; }
    public Address Address { get; private set; } = default!;

    public CompanyProfile? CompanyProfile { get; private set; }

    private CompanyLocation() { }

    private CompanyLocation(Guid companyProfileId, Address address)
    {
        CompanyProfileId = companyProfileId;
        Address = address;
    }

    public static Result<CompanyLocation> Create(Guid companyProfileId, Address address)
    {
        List<Error> errors = [];

        if (companyProfileId == Guid.Empty)
        {
            errors.Add(CompanyLocationError.CompanyProfileIdRequired);
        }

        if (address == null)
        {
            errors.Add(CompanyLocationError.AddressRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new CompanyLocation(companyProfileId, address!);
    }
}

public static class CompanyLocationError
{
    public static Error CompanyProfileIdRequired => Error.Validation("CompanyLocation_CompanyProfileId_Required", "Company profile id is required");
    public static Error AddressRequired => Error.Validation("CompanyLocation_Address_Required", "Address is required");
}