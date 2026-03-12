using JobLink.Domain.Common;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.ValueObjects;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobApplications;

namespace JobLink.Domain.Companies.Jobs;

public sealed class Job : Entity
{
    public Guid CompanyProfileId { get; private set; }
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string? Requirements { get; private set; }
    public JobType JobType { get; private set; }
    public JobLocationType LocationType { get; private set; }
    public Address Location { get; private set; } = default!;
    public SalaryRange SalaryRange { get; private set; } = default!;
    public ExperienceLevel ExperienceLevel { get; private set; } = default!;
    public DateTime PostedAtUtc { get; private set; }
    public DateTime? ClosedAtUtc { get; private set; }
    public DateTime ExpirationDateUtc { get; private set; }
    public JobStatus Status { get; private set; }

    public CompanyProfile? CompanyProfile { get; private set; }
    private readonly List<JobApplication> _applications = [];
    public IReadOnlyCollection<JobApplication> Applications => _applications;

    private readonly List<JobSkill> _skills = [];
    public IReadOnlyCollection<JobSkill> Skills => _skills;

    private Job() { }

    private Job(Guid companyProfileId, string title, string description, string? requirements, JobType jobType, JobLocationType locationType, Address location, SalaryRange salaryRange, ExperienceLevel experienceLevel, DateTime postedAtUtc, DateTime? closedAtUtc, DateTime expirationDateUtc, JobStatus status)
    {
        CompanyProfileId = companyProfileId;
        Title = title;
        Description = description;
        Requirements = requirements;
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

    public static Result<Job> Create(Guid companyProfileId, string title, string description, string? requirements, JobType jobType, JobLocationType locationType, Address location, SalaryRange salaryRange, ExperienceLevel experienceLevel, DateTime postedAtUtc, DateTime? closedAtUtc, DateTime expirationDateUtc, JobStatus status)
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

        return new Job(companyProfileId, title, description, requirements, jobType, locationType, location!, salaryRange!, experienceLevel, postedAtUtc, closedAtUtc, expirationDateUtc, status);
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
