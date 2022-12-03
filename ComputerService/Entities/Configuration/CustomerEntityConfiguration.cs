using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired()
            .HasMaxLength(62);
        builder.Property(x => x.PhoneNumber).IsRequired()
            .HasMaxLength(15);

        builder.HasOne(x => x.Address)
            .WithOne(x => x.Customer)
            .HasForeignKey<Address>(x => x.Id);

        builder.HasMany(x => x.Devices)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);
    }
}