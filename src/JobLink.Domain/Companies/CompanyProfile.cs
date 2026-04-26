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
    public string? Description { get; private set; }
    public string? LogoUrl { get; private set; }
    public string? Website { get; private set; }

    public User? User { get; private set; }
    private readonly List<CompanyLocation> _companyLocations = [];
    public IReadOnlyCollection<CompanyLocation> CompanyLocations => _companyLocations;
    private readonly List<Job> _jobs = [];
    public IReadOnlyCollection<Job> Jobs => _jobs;

    private CompanyProfile() { }

    private CompanyProfile(Guid userId, string name, string industry)
    {
        Name = name;
        Industry = industry;
        UserId = userId;
    }

    public static Result<CompanyProfile> Create(Guid userId, string name, string industry)
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

        return new CompanyProfile(userId, name, industry);
    }

    public Result Update(string? name, string? industry, string? description, string? logoUrl, string? website)
    {
        List<Error> errors = [];

        if (name is not null)
        {
            Name = name;
        }

        if (industry is not null)
        {
            Industry = industry;
        }

        if (description is not null)
        {
            Description = description;
        }

        if (logoUrl is not null)
        {
            LogoUrl = logoUrl;
        }

        if (website is not null)
        {
            Website = website;
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return Result.Success();
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