using ComputerService.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(o => o.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(o => o.ClientId).IsRequired();
        builder.Property(o => o.Status).HasDefaultValue(OrderStatusEnum.Pending);

        builder.HasOne(o => o.Client)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.ClientId);

        builder.HasMany(o => o.Accessories)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId);

        builder.HasOne(o => o.Details)
            .WithOne(o => o.Order)
            .HasForeignKey<OrderDetails>(o => o.Id);

        builder.HasOne(o => o.CreateUser)
            .WithMany(o => o.CreatedOrders)
            .HasForeignKey(o => o.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(o => o.ServiceUser)
            .WithMany(o => o.ServicedOrders)
            .HasForeignKey(o => o.ServicedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(o => o.CompleteUser)
            .WithMany(o => o.CompletedOrders)
            .HasForeignKey(o => o.CompletedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
