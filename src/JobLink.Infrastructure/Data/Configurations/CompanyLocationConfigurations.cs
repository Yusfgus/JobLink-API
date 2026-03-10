using JobLink.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobLink.Infrastructure.Data.Configurations;

public class CompanyLocationConfiguration : EntityConfiguration<CompanyLocation>
{
    public override void Configure(EntityTypeBuilder<CompanyLocation> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyLocations");

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Country).IsRequired();
            addressBuilder.Property(a => a.City).IsRequired();
            addressBuilder.Property(a => a.Area).IsRequired(false);
        });

        builder.HasOne(x => x.CompanyProfile)
            .WithMany(x => x.CompanyLocations)
            .HasForeignKey(x => x.CompanyProfileId)
            .IsRequired();
    }
}
