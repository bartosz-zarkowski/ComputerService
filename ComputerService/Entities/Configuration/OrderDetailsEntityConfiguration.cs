using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class OrderDetailsEntityConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.ServiceDescription).HasMaxLength(500);
        builder.Property(x => x.ServiceCharges).HasPrecision(10, 2);
        builder.Property(x => x.HardwareCharges).HasPrecision(10, 2);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Details)
            .HasForeignKey<OrderDetails>(x => x.Id);
    }
}
