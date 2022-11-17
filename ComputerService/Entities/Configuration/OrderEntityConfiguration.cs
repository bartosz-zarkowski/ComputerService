using ComputerService.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).HasDefaultValue(OrderStatusEnum.Pending)
            .HasConversion(
                v => v.ToString(),
                v => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), v));
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Accessories)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.Details)
            .WithOne(x => x.Order)
            .HasForeignKey<OrderDetails>(x => x.Id);

        builder.HasOne(x => x.CreateUser)
            .WithMany(x => x.CreatedOrders)
            .HasForeignKey(x => x.CreatedBy);

        builder.HasOne(x => x.ServiceUser)
            .WithMany(x => x.ServicedOrders)
            .HasForeignKey(x => x.ServicedBy);

        builder.HasOne(x => x.CompleteUser)
            .WithMany(x => x.CompletedOrders)
            .HasForeignKey(x => x.CompletedBy);
    }
}