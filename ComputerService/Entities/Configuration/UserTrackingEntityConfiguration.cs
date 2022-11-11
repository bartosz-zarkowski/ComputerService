using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class UserTrackingEntityConfiguration : IEntityTypeConfiguration<UserTracking>
{
    public void Configure(EntityTypeBuilder<UserTracking> builder)
    {
        builder.HasNoKey();
        builder.Property(x => x.Action).IsRequired();
        builder.Property(x => x.Date).ValueGeneratedOnAdd();
    }
}