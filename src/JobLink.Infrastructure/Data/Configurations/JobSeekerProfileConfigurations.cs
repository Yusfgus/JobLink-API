using JobLink.Domain.JobSeekers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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
                .HasMaxLength(100)
                .IsRequired();

            nameBuilder.Property(x => x.MiddleName)
                .HasMaxLength(100);

            nameBuilder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(x => x.Country)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Property(x => x.MobileNumber)
            .HasMaxLength(15);

        builder.Property(x => x.Nationality)
            .HasMaxLength(100);

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
