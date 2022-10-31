using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class UserTrackingEntityConfiguration : IEntityTypeConfiguration<UserTracking>
{
    public void Configure(EntityTypeBuilder<UserTracking> builder)
    {
        builder.HasNoKey();
        builder.Property(o => o.Action).IsRequired();
        builder.Property(o => o.Date).ValueGeneratedOnAdd();
    }
}