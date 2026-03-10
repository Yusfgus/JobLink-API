using JobLink.Domain.SavedJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class SavedJobConfiguration : EntityConfiguration<SavedJob>
{
    public override void Configure(EntityTypeBuilder<SavedJob> builder)
    {
        base.Configure(builder);

        builder.ToTable("SavedJobs");

        builder.Property(x => x.SavedAtUtc)
            .IsRequired();

        builder.HasOne(x => x.JobSeekerProfile)
            .WithMany(x => x.SavedJobs)
            .HasForeignKey(x => x.JobSeekerProfileId)
            .IsRequired();

        builder.HasOne(x => x.Job)
            .WithMany()
            .HasForeignKey(x => x.JobId)
            .IsRequired();
    }
}
