using ComputerService.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ComputerService.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotNull()
            .MaximumLength(50);
        RuleFor(x => x.LastName).NotNull()
            .MaximumLength(50); ;
        RuleFor(x => x.Email).EmailAddress()
            .MaximumLength(62);
        RuleFor(x => x.PhoneNumber).NotNull()
            .MinimumLength(8)
            .MaximumLength(15)
            .Matches(new Regex(@"^\d{8,15}$")).WithMessage("Incorrect phone number");
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.Role)
            .IsInEnum();
    }
}
