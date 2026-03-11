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

        builder.OwnsOne(x => x.Name, nameBuilder =>
        {
            nameBuilder.Property(x => x.FirstName)
                .HasMaxLength(JobSeekerProfileConstraints.FirstNameMaxLength)
                .IsRequired();

            nameBuilder.Property(x => x.MiddleName)
                .HasMaxLength(JobSeekerProfileConstraints.MiddleNameMaxLength);

            nameBuilder.Property(x => x.LastName)
                .HasMaxLength(JobSeekerProfileConstraints.LastNameMaxLength)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.City)
                .HasMaxLength(AddressConstraints.CityMaxLength)
                .IsRequired();

            addressBuilder.Property(x => x.Country)
                .HasMaxLength(AddressConstraints.CountryMaxLength)
                .IsRequired();
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
            .IsRequired();

    }
}
