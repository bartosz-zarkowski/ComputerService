using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class OrderAccessoryValidator : AbstractValidator<OrderAccessory>
{
    public OrderAccessoryValidator()
    {
        RuleFor(x => x.Name).NotNull()
            .MaximumLength(50);
    }
}
