using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.Companies;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class CompanyProfileConfiguration : EntityConfiguration<CompanyProfile>
{
    public override void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyProfiles");

        builder.Property(x => x.Name)
            .HasMaxLength(CompanyProfileConstraints.NameMaxLength)
            .IsRequired();

        builder.Property(x => x.Industry)
            .HasMaxLength(CompanyProfileConstraints.IndustryMaxLength)
            .IsRequired();

        builder.Property(x => x.Website)
            .HasMaxLength(CompanyProfileConstraints.WebsiteMaxLength);

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<CompanyProfile>(x => x.UserId)
            .IsRequired();
    }
}