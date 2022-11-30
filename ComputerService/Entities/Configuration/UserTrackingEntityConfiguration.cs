using ComputerService.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class UserTrackingEntityConfiguration : IEntityTypeConfiguration<UserTracking>
{
    public void Configure(EntityTypeBuilder<UserTracking> builder)
    {
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.TrackingActionType).IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (TrackingActionTypeEnum)Enum.Parse(typeof(TrackingActionTypeEnum), v));
        builder.Property(x => x.ActionTargetId).IsRequired();
    }
}