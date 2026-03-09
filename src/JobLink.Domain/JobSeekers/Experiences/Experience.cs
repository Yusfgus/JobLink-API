using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;

namespace JobLink.Domain.JobSeekers.Experiences;

public sealed class Experience : Entity
{
    public Guid JobSeekerProfileId { get; }
    public string Company { get; } = default!;
    public string Position { get; } = default!;
    public string Country { get; } = default!;
    public string? Description { get; }
    public decimal Salary { get; }
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }

    public JobSeekerProfile? JobSeekerProfile { get; }

    private Experience() { }

    private Experience(Guid jobSeekerProfileId, string company, string position, string country, string? description, decimal salary, DateOnly startDate, DateOnly endDate)
    {
        JobSeekerProfileId = jobSeekerProfileId;
        Company = company;
        Position = position;
        Country = country;
        Description = description;
        Salary = salary;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Result<Experience> Create(Guid jobSeekerProfileId, string company, string position, string country, string? description, decimal salary, DateOnly startDate, DateOnly endDate)
    {
        List<Error> errors = [];

        if (jobSeekerProfileId == Guid.Empty)
        {
            errors.Add(ExperienceError.JobSeekerProfileIdRequired);
        }

        if (string.IsNullOrWhiteSpace(company))
        {
            errors.Add(ExperienceError.CompanyRequired);
        }

        if (string.IsNullOrWhiteSpace(position))
        {
            errors.Add(ExperienceError.PositionRequired);
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            errors.Add(ExperienceError.CountryRequired);
        }

        if (startDate == default)
        {
            errors.Add(ExperienceError.StartDateRequired);
        }
        else if (startDate > DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(ExperienceError.StartDateMustBeInPast);
        }

        if (endDate == default)
        {
            errors.Add(ExperienceError.EndDateRequired);
        }
        else if (endDate < startDate)
        {
            errors.Add(ExperienceError.EndDateMustBeAfterStartDate);
        }
        else if (endDate > DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(ExperienceError.EndDateMustBeInPast);
        }

        if (salary < 0)
        {
            errors.Add(ExperienceError.SalaryMustBeNonNegative);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Experience(jobSeekerProfileId, company, position, country, description, salary, startDate, endDate);
    }
}

public static class ExperienceError
{
    public static Error JobSeekerProfileIdRequired => Error.Validation("Experience_JobSeekerProfileId_Required", "JobSeekerProfileId is required");
    public static Error CompanyRequired => Error.Validation("Experience_Company_Required", "Company is required");
    public static Error PositionRequired => Error.Validation("Experience_Position_Required", "Position is required");
    public static Error CountryRequired => Error.Validation("Experience_Country_Required", "Country is required");
    public static Error StartDateRequired => Error.Validation("Experience_StartDate_Required", "StartDate is required");
    public static Error EndDateRequired => Error.Validation("Experience_EndDate_Required", "EndDate is required");
    public static Error StartDateMustBeInPast => Error.Validation("Experience_StartDate_MustBeInPast", "StartDate must be in past");
    public static Error EndDateMustBeInPast => Error.Validation("Experience_EndDate_MustBeInPast", "EndDate must be in past");
    public static Error EndDateMustBeAfterStartDate => Error.Validation("Experience_EndDate_MustBeAfterStartDate", "EndDate must be after StartDate");
    public static Error SalaryMustBeNonNegative => Error.Validation("Experience_Salary_MustBeNonNegative", "Salary must be non-negative");
}