﻿using ComputerService.Entities.Enums;

namespace ComputerService.Models;
public class UpdateUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public UserRoleEnum Role { get; set; }
}
