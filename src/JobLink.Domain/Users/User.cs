using System.Net.Mail;
using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Enums;

namespace JobLink.Domain.Users;

public sealed class User : Entity
{
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string? ProfilePictureUrl { get; private set; }
    public UserRole Role { get; private set; }
    public string? Summary { get; private set; }

    private User() { }

    private User(string email, string passwordHash, string? profilePictureUrl, UserRole role, string? summary)
    {
        Email = email;
        PasswordHash = passwordHash;
        ProfilePictureUrl = profilePictureUrl;
        Role = role;
        Summary = summary;
    }

    public static Result<User> Create(string email, string passwordHash, string? profilePictureUrl, UserRole role, string? summary)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add(UserError.EmailRequired);
        }
        else
        {
            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                errors.Add(UserError.EmailInvalid);
            }
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            errors.Add(UserError.PasswordRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new User(email, passwordHash, profilePictureUrl, role, summary);
    }
}

public static class UserError
{
    public static Error EmailRequired => Error.Validation("User_Email_Required", "Email is required");
    public static Error EmailInvalid => Error.Validation("User_Email_Invalid", "Email is invalid");
    public static Error PasswordRequired => Error.Validation("User_Password_Required", "Password is required");
}