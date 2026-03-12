using JobLink.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobLink.Domain.Common.Constants;

namespace JobLink.Infrastructure.Data.Configurations;

public class CompanyLocationConfiguration : EntityConfiguration<CompanyLocation>
{
    public override void Configure(EntityTypeBuilder<CompanyLocation> builder)
    {
        base.Configure(builder);

        builder.ToTable("CompanyLocations");

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Country)
                .HasMaxLength(AddressConstraints.CountryMaxLength)
                .HasColumnName("Country")
                .IsRequired();

            addressBuilder.Property(a => a.City)
                .HasMaxLength(AddressConstraints.CityMaxLength)
                .HasColumnName("City")
                .IsRequired();

            addressBuilder.Property(a => a.Area)
                .HasMaxLength(AddressConstraints.AreaMaxLength)
                .HasColumnName("Area")
                .IsRequired(false);
        });

        builder.HasOne(x => x.CompanyProfile)
            .WithMany(x => x.CompanyLocations)
            .HasForeignKey(x => x.CompanyProfileId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
