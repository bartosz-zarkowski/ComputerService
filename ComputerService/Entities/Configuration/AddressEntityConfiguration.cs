using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(o => o.Id).IsRequired();
        builder.Property(o => o.Country).IsRequired();
        builder.Property(o => o.State).IsRequired();
        builder.Property(o => o.City).IsRequired();
        builder.Property(o => o.PostalCode).IsRequired();
        builder.Property(o => o.Street).IsRequired();
        builder.Property(o => o.StreetNumber).IsRequired();

        builder.HasOne(o => o.Client)
            .WithOne(o => o.Address)
            .HasForeignKey<Address>(o => o.Id);
    }
}
