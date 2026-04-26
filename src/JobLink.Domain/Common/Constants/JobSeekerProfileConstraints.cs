namespace JobLink.Domain.Common.Constants;

public static class JobSeekerProfileConstraints
{
    public const int FirstNameMaxLength = 100;
    public const int FirstNameMinLength = 2;
    public const int MiddleNameMaxLength = 100;
    public const int MiddleNameMinLength = 2;
    public const int LastNameMaxLength = 100;
    public const int LastNameMinLength = 2;
    public const int MobileNumberMaxLength = 20;
    public const int NationalityMaxLength = 100;
    public const int NationalityMinLength = 2;
    public const string MobileNumberRegex = @"^\+?\d{7,15}$";
    public const int SummaryMaxLength = 1000;
    public const int ProfilePictureUrlMaxLength = 500;
}
