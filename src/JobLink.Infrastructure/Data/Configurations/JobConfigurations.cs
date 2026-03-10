using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.Companies.Jobs;

namespace JobLink.Infrastructure.Data.Configurations;

public class JobConfiguration : EntityConfiguration<Job>
{
    public override void Configure(EntityTypeBuilder<Job> builder)
    {
        base.Configure(builder);

        builder.ToTable("Jobs");

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Requirements)
            .HasMaxLength(100);

        builder.Property(x => x.JobType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.LocationType)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(x => x.Location, locationBuilder =>
        {
            locationBuilder.Property(a => a.Country).IsRequired();
            locationBuilder.Property(a => a.City).IsRequired();
            locationBuilder.Property(a => a.Area).IsRequired();
        });

        builder.OwnsOne(x => x.SalaryRange, salaryRangeBuilder =>
        {
            salaryRangeBuilder.Property(sr => sr.Min).HasColumnType("decimal(18,2)").IsRequired();
            salaryRangeBuilder.Property(sr => sr.Max).HasColumnType("decimal(18,2)").IsRequired();
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