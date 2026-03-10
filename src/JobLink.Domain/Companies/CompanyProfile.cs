using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Users;

namespace JobLink.Domain.Companies;

public sealed class CompanyProfile : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Industry { get; private set; } = default!;
    public string? Website { get; private set; }

    public User? User { get; private set; }
    public IEnumerable<CompanyLocation> CompanyLocations { get; private set; } = [];
    public IEnumerable<Job> Jobs { get; private set; } = [];

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
}

public static class CompanyProfileError
{
    public static Error NameRequired => Error.Validation("CompanyProfile_Name_Required", "Name is required");
    public static Error IndustryRequired => Error.Validation("CompanyProfile_Industry_Required", "Industry is required");
}