using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class AccessoryEntityConfiguration : IEntityTypeConfiguration<Accessory>
{
    public void Configure(EntityTypeBuilder<Accessory> builder)
    {
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(o => o.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.OrderId).IsRequired();

        builder.HasOne(o => o.Order)
            .WithMany(o => o.Accessories)
            .HasForeignKey(o => o.OrderId);
    }
}
