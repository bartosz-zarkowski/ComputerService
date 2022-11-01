using ComputerService.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ComputerService.Validators;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
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
    }
}
