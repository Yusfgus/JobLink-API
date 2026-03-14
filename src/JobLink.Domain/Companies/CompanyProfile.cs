using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.ValueObjects;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Identity;

namespace JobLink.Domain.Companies;

public sealed class CompanyProfile : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Industry { get; private set; } = default!;
    public string? Website { get; private set; }

    public User? User { get; private set; }
    private readonly List<CompanyLocation> _companyLocations = [];
    public IReadOnlyCollection<CompanyLocation> CompanyLocations => _companyLocations;
    private readonly List<Job> _jobs = [];
    public IReadOnlyCollection<Job> Jobs => _jobs;

    private CompanyProfile() { }

    private CompanyProfile(Guid userId, string name, string industry, string? website)
    {
        Name = name;
        Industry = industry;
        Website = website;
        UserId = userId;
    }

    public static Result<CompanyProfile> Create(Guid userId, string name, string industry, string? website)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(CompanyProfileError.NameRequired);
        }

        if (string.IsNullOrWhiteSpace(industry))
        {
            errors.Add(CompanyProfileError.IndustryRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new CompanyProfile(userId, name, industry, website);
    }

    public static Result<CompanyProfile> Register(Guid userId, string name, string industry)
    {
        return Create(userId, name, industry, null);
    }

    public Result AddLocation(Address location)
    {
        var companyLocationResult = CompanyLocation.Create(Id, location);

        if (companyLocationResult.IsFailure)
        {
            return companyLocationResult.Errors;
        }

        _companyLocations.Add(companyLocationResult.Value!);

        return Result.Success();
    }

    public Result AddLocations(List<Address> locations)
    {
        var companyLocationsResults = locations.Select(location => CompanyLocation.Create(Id, location));

        if (companyLocationsResults.Any(x => x.IsFailure))
        {
            return companyLocationsResults.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
        }

        _companyLocations.AddRange(companyLocationsResults.Select(x => x.Value!));

        return Result.Success();
    }
}

public static class CompanyProfileError
{
    public static Error NameRequired => Error.Validation("CompanyProfile_Name_Required", "Name is required");
    public static Error IndustryRequired => Error.Validation("CompanyProfile_Industry_Required", "Industry is required");
}