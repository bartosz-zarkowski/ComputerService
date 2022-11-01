using ComputerService.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.ClientId).IsRequired();
        builder.Property(x => x.Status).HasDefaultValue(OrderStatusEnum.Pending);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.Accessories)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.Details)
            .WithOne(x => x.Order)
            .HasForeignKey<OrderDetails>(x => x.Id);

        builder.HasOne(x => x.CreateUser)
            .WithMany(x => x.CreatedOrders)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.ServiceUser)
            .WithMany(x => x.ServicedOrders)
            .HasForeignKey(x => x.ServicedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.CompleteUser)
            .WithMany(x => x.CompletedOrders)
            .HasForeignKey(x => x.CompletedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
