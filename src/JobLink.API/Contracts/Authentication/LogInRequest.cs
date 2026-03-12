namespace JobLink.API.Contracts.Authentication;

public record LogInRequest(
    string Email,
    string Password
);