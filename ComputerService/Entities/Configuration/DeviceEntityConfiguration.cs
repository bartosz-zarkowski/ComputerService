using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class DeviceEntityConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(r => r.Name).IsRequired();
        builder.Property(r => r.HasWarranty).IsRequired();
        builder.Property(r => r.ClientId).IsRequired();
        builder.Property(r => r.OrderId).IsRequired();

        builder.HasOne(o => o.Client)
            .WithMany(o => o.Devices)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.Order)
            .WithMany(o => o.Devices)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
