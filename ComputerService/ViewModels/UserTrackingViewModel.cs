using ComputerService.Entities.Enums;

namespace ComputerService.ViewModels;

public class UserTrackingViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public TrackingActionTypeEnum TrackingActionType { get; set; }
    public string ActionTargetId { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset Date { get; set; }
}