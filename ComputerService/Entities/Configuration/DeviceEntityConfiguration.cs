using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class DeviceEntityConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.SerialNumber).HasMaxLength(50);
        builder.Property(x => x.Condition).HasMaxLength(500);
        builder.Property(x => x.HasWarranty).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}