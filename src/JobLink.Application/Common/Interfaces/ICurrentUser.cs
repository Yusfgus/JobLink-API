namespace JobLink.Application.Common.Interfaces;

public interface ICurrentUser
{
    Guid? Id { get; }
    string? Email { get; }
    string? Role { get; }
}