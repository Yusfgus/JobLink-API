using JobLink.Domain.JobApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class JobApplicationConfiguration : EntityConfiguration<JobApplication>
{
    public override void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        base.Configure(builder);

        builder.ToTable("JobApplications");

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.JobId)
            .IsRequired();
    }
}
