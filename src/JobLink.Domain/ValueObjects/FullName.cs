using JobLink.Domain.Common.Results;

namespace JobLink.Domain.ValueObjects;

public record FullName
{
    public string FirstName { get; } = default!;
    public string? MiddleName { get; }
    public string LastName { get; } = default!;

    private FullName() {}
    private FullName(string firstName, string? middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string firstName, string? middleName, string lastName)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(FullNameError.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(FullNameError.LastNameRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new FullName(firstName, middleName, lastName);
    }
}

public static class FullNameError
{
    public static Error FirstNameRequired => Error.Validation("FullName_FirstName", "First name is required");
    public static Error LastNameRequired => Error.Validation("FullName_LastName", "Last name is required");
}