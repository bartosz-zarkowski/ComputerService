using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Title).MaximumLength(70);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.CreatedBy).NotNull();
    }
}
