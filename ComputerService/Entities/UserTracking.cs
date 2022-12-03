using ComputerService.Entities.Enums;

namespace ComputerService.Entities;
public class UserTracking
{
    public UserTracking(string firstName, string lastName, TrackingActionTypeEnum trackingActionType, string actionTargetId, DateTimeOffset date, string? description)
    {
        FirstName = firstName;
        LastName = lastName;
        TrackingActionType = trackingActionType;
        ActionTargetId = actionTargetId;
        Date = date;
        Description = description;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public TrackingActionTypeEnum TrackingActionType { get; set; }
    public string ActionTargetId { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset Date { get; set; }
}