namespace JobLink.Domain.Common.Constants;

public static class UserConstraints
{
    public const int EmailMaxLength = 256;
    public const int PasswordHashMinLength = 8;
    public const int PasswordHashMaxLength = 100;
    public const int ProfilePictureUrlMaxLength = 256;
    public const int SummaryMaxLength = 500;
}