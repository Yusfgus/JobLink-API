using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Identity;
using JobLink.Domain.Common.ValueObjects;
using JobLink.Domain.JobApplications;
using JobLink.Domain.SavedJobs;

namespace JobLink.Domain.JobSeekers;

public sealed class JobSeekerProfile : Entity
{
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; } = default!;
    public string? MobileNumber { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public Address Address { get; private set; } = default!;
    public Gender Gender { get; private set; }
    public string? Nationality { get; private set; }
    public MilitaryStatus? MilitaryStatus { get; private set; }
    public MaritalStatus? MaritalStatus { get; private set; }

    public User? User { get; private set; }
    public Resume? Resume { get; private set; }
    private readonly List<Education> _educations = [];
    public IReadOnlyCollection<Education> Educations => _educations;

    private readonly List<Experience> _experiences = [];
    public IReadOnlyCollection<Experience> Experiences => _experiences;

    private readonly List<JobSeekerSkill> _skills = [];
    public IReadOnlyCollection<JobSeekerSkill> Skills => _skills;

    private readonly List<JobApplication> _applications = [];
    public IReadOnlyCollection<JobApplication> Applications => _applications;

    private readonly List<SavedJob> _savedJobs = [];
    public IReadOnlyCollection<SavedJob> SavedJobs => _savedJobs;

    public string FullName => $"{FirstName} {MiddleName ?? ""} {LastName}";

    private JobSeekerProfile() { }
    private JobSeekerProfile(Guid userId, string firstName, string? middleName, string lastName, string? mobileNumber, DateOnly? birthDate, Address address, Gender gender, string? nationality, MilitaryStatus? militaryStatus, MaritalStatus? maritalStatus)
    {
        UserId = userId;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        BirthDate = birthDate;
        Address = address;
        Gender = gender;
        Nationality = nationality;
        MilitaryStatus = militaryStatus;
        MaritalStatus = maritalStatus;
    }

    public JobSeekerProfile(Guid userId, string firstName, string lastName, Gender gender)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
    }

    public static Result<JobSeekerProfile> Create(Guid userId, string firstName, string? middleName, string lastName, Gender gender, string? mobileNumber, DateOnly? birthDate, Address address, string? nationality, MilitaryStatus? militaryStatus, MaritalStatus? maritalStatus)
    {
        List<Error> errors = [];

        if (userId == Guid.Empty)
        {
            errors.Add(JobSeekerProfileError.UserIdRequired);
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(JobSeekerProfileError.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(JobSeekerProfileError.LastNameRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobSeekerProfile(userId, firstName, middleName, lastName, mobileNumber, birthDate, address, gender, nationality, militaryStatus, maritalStatus);
    }

    public static Result<JobSeekerProfile> Register(Guid userId, string firstName, string lastName, Gender gender)
    {
        List<Error> errors = [];

        if (userId == Guid.Empty)
        {
            errors.Add(JobSeekerProfileError.UserIdRequired);
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(JobSeekerProfileError.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(JobSeekerProfileError.LastNameRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new JobSeekerProfile(userId, firstName, lastName, gender);
    }

}

internal static class JobSeekerProfileError
{
    public static Error UserIdRequired => Error.Validation("JobSeekerProfile.UserId", "User id is required");
    public static Error FirstNameRequired => Error.Validation("FullName.FirstName", "First name is required.");
    public static Error LastNameRequired => Error.Validation("FullName.LastName", "Last name is required.");

}