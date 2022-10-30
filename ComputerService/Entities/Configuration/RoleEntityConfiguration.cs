using ComputerService.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(r => r.FirstName).IsRequired();
        builder.Property(r => r.LastName).IsRequired();
        builder.Property(r => r.Email).IsRequired();
        builder.Property(r => r.Password).IsRequired();
        builder.Property(r => r.PhoneNumber).IsRequired();
        builder.Property(r => r.IsActive).HasDefaultValue(true);
        builder.Property(r => r.Role).HasDefaultValue(UserRoleEnum.Technician);
    }
}
