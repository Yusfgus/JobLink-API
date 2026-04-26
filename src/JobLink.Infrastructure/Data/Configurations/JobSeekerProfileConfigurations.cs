using JobLink.Domain.JobSeekers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class JobSeekerProfileConfiguration : EntityConfiguration<JobSeekerProfile>
{
    public override void Configure(EntityTypeBuilder<JobSeekerProfile> builder)
    {
        base.Configure(builder);

        builder.ToTable("JobSeekerProfiles");

        builder.Property(x => x.FirstName)
                .HasMaxLength(JobSeekerProfileConstraints.FirstNameMaxLength)
                .IsRequired();

        builder.Property(x => x.MiddleName)
            .HasMaxLength(JobSeekerProfileConstraints.MiddleNameMaxLength);

        builder.Property(x => x.LastName)
            .HasMaxLength(JobSeekerProfileConstraints.LastNameMaxLength)
            .IsRequired();

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.Country)
                .HasColumnName("Country")
                .HasMaxLength(AddressConstraints.CountryMaxLength);

            addressBuilder.Property(x => x.City)
                .HasColumnName("City")
                .HasMaxLength(AddressConstraints.CityMaxLength);

            addressBuilder.Property(x => x.Area)
                .HasColumnName("Area")
                .HasMaxLength(AddressConstraints.AreaMaxLength);
        });

        builder.Property(x => x.MobileNumber)
            .HasMaxLength(JobSeekerProfileConstraints.MobileNumberMaxLength);

        builder.Property(x => x.Nationality)
            .HasMaxLength(JobSeekerProfileConstraints.NationalityMaxLength);

        builder.Property(x => x.Gender)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.MilitaryStatus)
            .HasConversion<string>();

        builder.Property(x => x.MaritalStatus)
            .HasConversion<string>();

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<JobSeekerProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(UserConstraints.ProfilePictureUrlMaxLength);

        builder.Property(x => x.Summary)
            .HasMaxLength(UserConstraints.SummaryMaxLength);

    }
}
