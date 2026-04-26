using JobLink.Domain.Common;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.ValueObjects;
using JobLink.Domain.Common.Results;
using JobLink.Domain.JobApplications;
using JobLink.Domain.SavedJobs;

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
    public int MinSalary { get; private set; } = default!;
    public int MaxSalary { get; private set; } = default!;
    public ExperienceLevel ExperienceLevel { get; private set; } = default!;
    public DateTime PostedAtUtc { get; private set; }
    public DateOnly? ClosedAt { get; private set; }
    public DateOnly ExpirationDate { get; private set; }
    public JobStatus Status { get; private set; }

    public CompanyProfile? CompanyProfile { get; private set; }
    private readonly List<JobApplication> _applications = [];
    public IReadOnlyCollection<JobApplication> Applications => _applications;

    private readonly List<SavedJob> _saves = [];
    public IReadOnlyCollection<SavedJob> Saves => _saves;

    private readonly List<JobSkill> _skills = [];
    public IReadOnlyCollection<JobSkill> Skills => _skills;

    private Job() { }

    private Job(Guid companyProfileId, string title, string description, string? requirements, JobType jobType, JobLocationType locationType, Address location, int minSalary, int maxSalary, ExperienceLevel experienceLevel, DateTime postedAtUtc, DateOnly expirationDate, JobStatus status)
    {
        CompanyProfileId = companyProfileId;
        Title = title;
        Description = description;
        Requirements = requirements;
        JobType = jobType;
        LocationType = locationType;
        Location = location;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
        ExperienceLevel = experienceLevel;
        PostedAtUtc = postedAtUtc;
        ExpirationDate = expirationDate;
        Status = status;
    }

    public static Result<Job> Create(Guid companyProfileId, string title, string description, string? requirements, JobType jobType, JobLocationType locationType, Address location, int minSalary, int maxSalary, ExperienceLevel experienceLevel, DateOnly expirationDate)
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

        if (jobType == default)
        {
            errors.Add(JobError.JobTypeRequired);
        }

        if (locationType == default)
        {
            errors.Add(JobError.LocationTypeRequired);
        }

        if (location == null)
        {
            errors.Add(JobError.LocationRequired);
        }

        if (minSalary < 0 || maxSalary < 0)
        {
            errors.Add(JobError.SalaryRangeRequired);
        }
        else if (minSalary > maxSalary)
        {
            errors.Add(JobError.SalaryRangeInvalid);
        }

        if (expirationDate < DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(JobError.ExpirationDateInvalid);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Job(companyProfileId, title, description, requirements, jobType, locationType, location!, minSalary, maxSalary, experienceLevel, DateTime.UtcNow, expirationDate, JobStatus.Opened);
    }

    public void Close()
    {
        if (Status == JobStatus.Closed)
        {
            return;
        }

        Status = JobStatus.Closed;
        ClosedAt = DateOnly.FromDateTime(DateTime.Now);
    }

    public Result Update(string? title, string? description, string? requirements, JobType? jobType, JobLocationType? locationType, string? country, string? city, string? area, int? minSalary, int? maxSalary, ExperienceLevel? experienceLevel, DateOnly? expirationDate)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            Title = title;
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            Description = description;
        }

        if (!string.IsNullOrWhiteSpace(requirements))
        {
            Requirements = requirements;
        }

        if (jobType.HasValue)
        {
            JobType = jobType.Value;
        }

        if (locationType.HasValue)
        {
            LocationType = locationType.Value;
        }

        if (country != null || city != null || area != null)
        {
            Location = new Address(country ?? Location.Country, city ?? Location.City, area ?? Location.Area);
        }

        if (minSalary.HasValue)
        {
            MinSalary = minSalary.Value;
        }

        if (maxSalary.HasValue)
        {
            MaxSalary = maxSalary.Value;
        }

        if (MinSalary > MaxSalary)
        {
            return JobError.SalaryRangeInvalid;
        }

        if (experienceLevel.HasValue)
        {
            ExperienceLevel = experienceLevel.Value;
        }

        if (expirationDate.HasValue)
        {
            if (expirationDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return JobError.ExpirationDateInvalid;
            }
            ExpirationDate = expirationDate.Value;
        }

        return Result.Success();
    }
}

public static class JobError
{
    public static Error CompanyProfileIdRequired => Error.Validation("Job_CompanyProfileId_Required", "Company profile id is required");
    public static Error TitleRequired => Error.Validation("Job_Title_Required", "Title is required");
    public static Error DescriptionRequired => Error.Validation("Job_Description_Required", "Description is required");
    public static Error JobTypeRequired => Error.Validation("Job_JobType_Required", "Job type is required");
    public static Error LocationTypeRequired => Error.Validation("Job_LocationType_Required", "Location type is required");
    public static Error LocationRequired => Error.Validation("Job_Location_Required", "Location is required");
    public static Error SalaryRangeRequired => Error.Validation("Job_SalaryRange_Required", "Salary range is required");
    public static Error SalaryRangeInvalid => Error.Validation("Job_SalaryRange_Invalid", "Salary range is invalid");
    public static Error ExpirationDateInvalid => Error.Validation("Job_ExpirationDate_Invalid", "Expiration date is invalid");
}
