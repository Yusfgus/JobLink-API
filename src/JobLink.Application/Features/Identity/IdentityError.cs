using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.Identity;

public static class IdentityError
{
    public static Error UserNotFound => Error.NotFound("User.NotFound", "User not found");
    public static Error InvalidCredentials => Error.Unauthorized("Identity.InvalidCredentials", "Invalid email or password");
    public static Error Unauthorized => Error.Unauthorized("Identity.Unauthorized", "User is not authenticated");
}