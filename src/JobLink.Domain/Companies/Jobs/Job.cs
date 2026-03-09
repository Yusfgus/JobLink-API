using JobLink.Domain.JobApplications;
using JobLink.Domain.Common;
using JobLink.Domain.Enums;
using JobLink.Domain.ValueObjects;
using JobLink.Domain.Common.Results;

namespace JobLink.Domain.Companies.Jobs;

public sealed class Job : Entity
{
    public Guid CompanyProfileId { get; }
    public string Title { get; } = default!;
    public string Description { get; } = default!;
    public JobType JobType { get; }
    public JobLocationType LocationType { get; }
    public Address Location { get; } = default!;
    public SalaryRange SalaryRange { get; } = default!;
    public ExperienceLevel ExperienceLevel { get; } = default!;
    public DateTime PostedAtUtc { get; }
    public DateTime? ClosedAtUtc { get; }
    public DateTime ExpirationDateUtc { get; }
    public JobStatus Status { get; }

    public CompanyProfile? CompanyProfile { get; }
    public IEnumerable<JobApplication> Applications { get; } = [];
    public IEnumerable<JobSkill> Skills { get; } = [];

    private Job() { }

    private Job(Guid companyProfileId, string title, string description, JobType jobType, JobLocationType locationType, Address location, SalaryRange salaryRange, ExperienceLevel experienceLevel, DateTime postedAtUtc, DateTime? closedAtUtc, DateTime expirationDateUtc, JobStatus status)
    {
        CompanyProfileId = companyProfileId;
        Title = title;
        Description = description;
        JobType = jobType;
        LocationType = locationType;
        Location = location;
        SalaryRange = salaryRange;
        ExperienceLevel = experienceLevel;
        PostedAtUtc = postedAtUtc;
        ClosedAtUtc = closedAtUtc;
        ExpirationDateUtc = expirationDateUtc;
        Status = status;
    }

    public static Result<Job> Create(Guid companyProfileId, string title, string description, JobType jobType, JobLocationType locationType, Address location, SalaryRange salaryRange, ExperienceLevel experienceLevel, DateTime postedAtUtc, DateTime? closedAtUtc, DateTime expirationDateUtc, JobStatus status)
    {
        List<Error> errors = [];

        if (companyProfileId == Guid.Empty)
        {
            errors.Add(JobError.CompanyProfileIdRequired);
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add(JobError.TitleRequired);
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            errors.Add(JobError.DescriptionRequired);
        }

        if (location == null)
        {
            errors.Add(JobError.LocationRequired);
        }

        if (salaryRange == null)
        {
            errors.Add(JobError.SalaryRangeRequired);
        }

        if (expirationDateUtc < DateTime.UtcNow)
        {
            errors.Add(JobError.ExpirationDateInvalid);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Job(companyProfileId, title, description, jobType, locationType, location!, salaryRange!, experienceLevel, postedAtUtc, closedAtUtc, expirationDateUtc, status);
    }
}

public static class JobError
{
    public static Error CompanyProfileIdRequired => Error.Validation("Job_CompanyProfileId_Required", "Company profile id is required");
    public static Error TitleRequired => Error.Validation("Job_Title_Required", "Title is required");
    public static Error DescriptionRequired => Error.Validation("Job_Description_Required", "Description is required");
    public static Error LocationRequired => Error.Validation("Job_Location_Required", "Location is required");
    public static Error SalaryRangeRequired => Error.Validation("Job_SalaryRange_Required", "Salary range is required");
    public static Error ExpirationDateInvalid => Error.Validation("Job_ExpirationDate_Invalid", "Expiration date is invalid");
}
