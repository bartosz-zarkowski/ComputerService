namespace ComputerService.Entities;
public class UserTracking
{
    public virtual User User { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }
}
