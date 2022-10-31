using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class OrderDetailsEntityConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.Property(r => r.Id).IsRequired();
        builder.Property(o => o.ServiceCharges).HasPrecision(10, 2);
        builder.Property(o => o.HardwareCharges).HasPrecision(10, 2);

        builder.HasOne(o => o.Order)
            .WithOne(o => o.Details)
            .HasForeignKey<OrderDetails>(o => o.Id);
    }
}
