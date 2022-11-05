using ComputerService.Entities.Enums;

namespace ComputerService.Models;

public class JwtUserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRoleEnum Role { get; set; }
}