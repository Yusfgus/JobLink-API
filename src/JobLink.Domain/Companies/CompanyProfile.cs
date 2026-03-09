using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Users;

namespace JobLink.Domain.Companies;

public sealed class CompanyProfile : Entity
{
    public Guid UserId { get; }
    public string Name { get; } = default!;
    public string Industry { get; } = default!;
    public string? Website { get; }

    public User? User { get; }
    public IEnumerable<CompanyLocation> Locations { get; } = [];
    public IEnumerable<Job> Jobs { get; } = [];

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