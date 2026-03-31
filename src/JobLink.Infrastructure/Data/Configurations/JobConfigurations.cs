using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class JobConfiguration : EntityConfiguration<Job>
{
    public override void Configure(EntityTypeBuilder<Job> builder)
    {
        base.Configure(builder);

        builder.ToTable("Jobs");

        builder.Property(x => x.Title)
            .HasMaxLength(JobConstraints.TitleMaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(JobConstraints.DescriptionMaxLength)
            .IsRequired();

        builder.Property(x => x.Requirements)
            .HasMaxLength(JobConstraints.RequirementsMaxLength);

        builder.Property(x => x.JobType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.LocationType)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(x => x.Location, locationBuilder =>
        {
            locationBuilder.Property(a => a.Country)
                .HasMaxLength(AddressConstraints.CountryMaxLength)
                .HasColumnName("Country")
                .IsRequired();

            locationBuilder.Property(a => a.City)
                .HasMaxLength(AddressConstraints.CityMaxLength)
                .HasColumnName("City")
                .IsRequired();

            locationBuilder.Property(a => a.Area)
                .HasMaxLength(AddressConstraints.AreaMaxLength)
                .HasColumnName("Area");
        });

        builder.Property(x => x.MinSalary).IsRequired();
        builder.Property(x => x.MaxSalary).IsRequired();

        builder.Property(x => x.ExperienceLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.PostedAtUtc)
            .IsRequired();

        builder.Property(x => x.ClosedAt);

        builder.Property(x => x.ExpirationDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.CompanyProfile)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.CompanyProfileId)
            .IsRequired();
    }
}