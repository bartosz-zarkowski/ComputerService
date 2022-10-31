using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired()
            .HasMaxLength(62);
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired()
            .HasMaxLength(18);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.Role).IsRequired();

        builder.HasMany(x => x.CreatedOrders)
            .WithOne(x => x.CreateUser)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.ServicedOrders)
            .WithOne(x => x.ServiceUser)
            .HasForeignKey(x => x.ServicedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.CompletedOrders)
            .WithOne(x => x.CompleteUser)
            .HasForeignKey(x => x.CompletedBy)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}