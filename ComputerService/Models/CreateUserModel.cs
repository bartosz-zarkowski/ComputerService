using ComputerService.Entities.Enums;

namespace ComputerService.Models;
public class CreateUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public UserRoleEnum Role { get; set; }
}
