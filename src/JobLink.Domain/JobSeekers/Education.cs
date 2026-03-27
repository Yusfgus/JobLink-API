using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.Enums;

namespace JobLink.Domain.JobSeekers;

public sealed class Education : Entity
{
    public Guid JobSeekerProfileId { get; private set; }
    public string Institution { get; private set; } = default!;
    public string Degree { get; private set; } = default!;
    public string FieldOfStudy { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public AcademicGrade Grade { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public JobSeekerProfile? JobSeekerProfile { get; private set; }

    private Education() { }

    private Education(Guid jobSeekerProfileId, string degree, string country, string institution, string fieldOfStudy, DateOnly startDate, DateOnly endDate, AcademicGrade grade)
    {
        JobSeekerProfileId = jobSeekerProfileId;
        Degree = degree;
        Country = country;
        Institution = institution;
        FieldOfStudy = fieldOfStudy;
        StartDate = startDate;
        EndDate = endDate;
        Grade = grade;
    }

    public static Result<Education> Create(Guid jobSeekerProfileId, string degree, string country, string institution, string fieldOfStudy, DateOnly startDate, DateOnly endDate, AcademicGrade grade)
    {
        List<Error> errors = [];

        if (jobSeekerProfileId == Guid.Empty)
        {
            errors.Add(EducationError.JobSeekerProfileIdRequired);
        }

        if (string.IsNullOrWhiteSpace(institution))
        {
            errors.Add(EducationError.InstitutionRequired);
        }

        if (string.IsNullOrWhiteSpace(degree))
        {
            errors.Add(EducationError.DegreeRequired);
        }

        if (string.IsNullOrWhiteSpace(fieldOfStudy))
        {
            errors.Add(EducationError.FieldOfStudyRequired);
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            errors.Add(EducationError.CountryRequired);
        }

        if (startDate == default)
        {
            errors.Add(EducationError.StartDateRequired);
        }
        else if (startDate > DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(EducationError.StartDateMustBeInPast);
        }

        if (endDate == default)
        {
            errors.Add(EducationError.EndDateRequired);
        }
        else if (endDate < startDate)
        {
            errors.Add(EducationError.EndDateMustBeAfterStartDate);
        }
        else if (endDate > DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(EducationError.EndDateMustBeInPast);
        }

        if (grade == default)
        {
            errors.Add(EducationError.GradeRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Education(jobSeekerProfileId, degree, country, institution, fieldOfStudy, startDate, endDate, grade);
    }

    public Result Update(string? degree, string? country, string? institution, string? fieldOfStudy, DateOnly? startDate, DateOnly? endDate, AcademicGrade? grade)
    {
        List<Error> errors = [];

        if (startDate.HasValue)
        {
            if (startDate.Value > DateOnly.FromDateTime(DateTime.Now))
            {
                errors.Add(EducationError.StartDateMustBeInPast);
            }
            else
            {
                StartDate = startDate.Value;
            }
        }
        
        if (endDate.HasValue)
        {
            if (endDate.Value > DateOnly.FromDateTime(DateTime.Now))
            {
                errors.Add(EducationError.EndDateMustBeInPast);
            }
            else
            {
                EndDate = endDate.Value;
            }
        }

        if (StartDate > EndDate)
        {
            errors.Add(EducationError.EndDateMustBeAfterStartDate);
        }

        if (!string.IsNullOrWhiteSpace(institution))
        {
            Institution = institution;
        }

        if (!string.IsNullOrWhiteSpace(degree))
        {
            Degree = degree;
        }

        if (!string.IsNullOrWhiteSpace(fieldOfStudy))
        {
            FieldOfStudy = fieldOfStudy;
        }

        if (!string.IsNullOrWhiteSpace(country))
        {
            Country = country;
        }

        if (grade.HasValue)
        {
            Grade = grade.Value;
        }
        
        if (errors.Count > 0)
        {
            return errors;
        }

        return Result.Success();
    }

}

public static class EducationError
{
    public static Error JobSeekerProfileIdRequired => Error.Validation("Education_JobSeekerProfileId_Required", "JobSeekerProfileId is required");
    public static Error InstitutionRequired => Error.Validation("Education_Institution_Required", "Institution is required");
    public static Error DegreeRequired => Error.Validation("Education_Degree_Required", "Degree is required");
    public static Error FieldOfStudyRequired => Error.Validation("Education_FieldOfStudy_Required", "FieldOfStudy is required");
    public static Error CountryRequired => Error.Validation("Education_Country_Required", "Country is required");
    public static Error StartDateRequired => Error.Validation("Education_StartDate_Required", "StartDate is required");
    public static Error EndDateRequired => Error.Validation("Education_EndDate_Required", "EndDate is required");
    public static Error StartDateMustBeInPast => Error.Validation("Education_StartDate_MustBeInPast", "StartDate must be in past");
    public static Error EndDateMustBeInPast => Error.Validation("Education_EndDate_MustBeInPast", "EndDate must be in past");
    public static Error EndDateMustBeAfterStartDate => Error.Validation("Education_EndDate_MustBeAfterStartDate", "EndDate must be after StartDate");
    public static Error GradeRequired => Error.Validation("Education_Grade_Required", "Grade is required");
}