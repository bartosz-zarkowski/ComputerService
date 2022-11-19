using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class OrderAccessoryEntityConfiguration : IEntityTypeConfiguration<OrderAccessory>
{
    public void Configure(EntityTypeBuilder<OrderAccessory> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.OrderId).IsRequired();

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Accessories)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}