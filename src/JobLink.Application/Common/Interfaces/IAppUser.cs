namespace JobLink.Application.Common.Interfaces;

public interface IAppUser
{
    Guid? UserId { get; }
    string? Email { get; }
    string? Role { get; }
    Guid? JobSeekerId { get; }
    Guid? CompanyId { get; }
}