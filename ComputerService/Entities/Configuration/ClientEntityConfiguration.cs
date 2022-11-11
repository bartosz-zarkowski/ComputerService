using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
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
            .WithOne(x => x.Client)
            .HasForeignKey<Address>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Devices)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}