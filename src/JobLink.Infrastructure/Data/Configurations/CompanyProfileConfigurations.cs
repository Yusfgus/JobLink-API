using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.Companies;

namespace JobLink.Infrastructure.Data.Configurations;

public class CompanyProfileConfiguration : EntityConfiguration<CompanyProfile>
{
    public override void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyProfiles");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Industry)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Website)
            .HasMaxLength(100);

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<CompanyProfile>(x => x.UserId)
            .IsRequired();
    }
}