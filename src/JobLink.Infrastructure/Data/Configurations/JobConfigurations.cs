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
                .IsRequired();

            locationBuilder.Property(a => a.City)
                .HasMaxLength(AddressConstraints.CityMaxLength)
                .IsRequired();

            locationBuilder.Property(a => a.Area)
                .HasMaxLength(AddressConstraints.AreaMaxLength)
                .IsRequired();
        });

        builder.OwnsOne(x => x.SalaryRange, salaryRangeBuilder =>
        {
            salaryRangeBuilder.Property(sr => sr.Min).HasColumnType("int(18,2)").IsRequired();
            salaryRangeBuilder.Property(sr => sr.Max).HasColumnType("int(18,2)").IsRequired();
        });

        builder.Property(x => x.ExperienceLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.PostedAtUtc)
            .IsRequired();

        builder.Property(x => x.ClosedAtUtc);

        builder.Property(x => x.ExpirationDateUtc)
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