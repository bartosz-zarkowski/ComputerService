using ComputerService.Models;
using FluentValidation;

namespace ComputerService.Validators;

public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequestModel>
{
    public AuthenticateRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress()
            .MaximumLength(62);
        RuleFor(x => x.Password).NotNull();
    }
}