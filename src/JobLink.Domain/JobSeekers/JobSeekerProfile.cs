using System.Text.RegularExpressions;
using JobLink.Domain.JobApplications;
using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Enums;
using JobLink.Domain.JobSeekers.Educations;
using JobLink.Domain.JobSeekers.Experiences;
using JobLink.Domain.JobSeekers.Resumes;
using JobLink.Domain.SavedJobs;
using JobLink.Domain.Users;
using JobLink.Domain.ValueObjects;

namespace JobLink.Domain.JobSeekers;

public sealed partial class JobSeekerProfile : Entity
{
    public Guid UserId { get; }
    public FullName Name { get; } = default!;
    public string? MobileNumber { get; }
    public DateOnly? BirthDate { get; }
    public Address? Address { get; }
    public Gender Gender { get; }
    public string? Nationality { get; }
    public MilitaryStatus? MilitaryStatus { get; }
    public MartialStatus? MaritalStatus { get; }

    public User? User { get; }
    public Resume? Resume { get; }
    public IEnumerable<Education> Educations { get; } = [];
    public IEnumerable<Experience> Experiences { get; } = [];
    public IEnumerable<JobSeekerSkill> Skills { get; } = [];
    public IEnumerable<JobApplication> Applications { get; } = [];
    public IEnumerable<SavedJob> SavedJobs { get; } = [];

    public string FullName => $"{Name.FirstName} {Name.MiddleName ?? ""} {Name.LastName}";

    [GeneratedRegex(@"^\+?\d{7,15}$")]
    private static partial Regex MobileNumberRegex();

    private JobSeekerProfile() { }
    private JobSeekerProfile(Guid userId, FullName name, string? mobileNumber, DateOnly? birthDate, Address? address, Gender gender, string? nationality, MilitaryStatus? militaryStatus, MartialStatus? maritalStatus)
    {
        UserId = userId;
        Name = name;
        MobileNumber = mobileNumber;
        BirthDate = birthDate;
        Address = address;
        Gender = gender;
        Nationality = nationality;
        MilitaryStatus = militaryStatus;
        MaritalStatus = maritalStatus;
    }
    public static Result<JobSeekerProfile> Create(Guid userId, FullName fullName, string? mobileNumber, DateOnly? birthDate, Address? address, Gender gender, string? nationality, MilitaryStatus? militaryStatus, MartialStatus? maritalStatus)
    {
        List<Error> errors = [];

        if (userId == Guid.Empty)
        {
            errors.Add(JobSeekerProfileError.UserIdRequired);
        }

        if (string.IsNullOrWhiteSpace(fullName.FirstName))
        {
            errors.Add(JobSeekerProfileError.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(fullName.LastName))
        {
            errors.Add(JobSeekerProfileError.LastNameRequired);
        }

        if (string.IsNullOrWhiteSpace(mobileNumber))
        {
            errors.Add(JobSeekerProfileError.MobileNumberRequired);
        }
        else if (!MobileNumberRegex().IsMatch(mobileNumber))
        {
            errors.Add(JobSeekerProfileError.MobileNumberInvalid);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobSeekerProfile(userId, fullName, mobileNumber, birthDate, address, gender, nationality, militaryStatus, maritalStatus);
    }

}

public static class JobSeekerProfileError
{
    public static Error UserIdRequired => Error.Validation("JobSeekerProfile_UserId_Required", "User id is required");
    public static Error FirstNameRequired => Error.Validation("JobSeekerProfile_FirstName_Required", "First name is required");
    public static Error LastNameRequired => Error.Validation("JobSeekerProfile_LastName_Required", "Last name is required");
    public static Error MobileNumberRequired => Error.Validation("JobSeekerProfile_MobileNumber_Required", "Mobile number is required");
    public static Error MobileNumberInvalid => Error.Validation("JobSeekerProfile_MobileNumber_Invalid", "Mobile number is invalid");
}