using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class AccessoryValidator : AbstractValidator<Accessory>
{
    public AccessoryValidator()
    {
        RuleFor(x => x.Name).NotNull()
            .MaximumLength(50);
    }
}
