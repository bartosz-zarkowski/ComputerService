using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(o => o.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(o => o.FirstName).IsRequired();
        builder.Property(o => o.LastName).IsRequired();

        builder.HasOne(o => o.Address)
            .WithOne(o => o.Client)
            .HasForeignKey<Address>(o => o.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Devices)
            .WithOne(o => o.Client)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(o => o.Orders)
            .WithOne(o => o.Client)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}