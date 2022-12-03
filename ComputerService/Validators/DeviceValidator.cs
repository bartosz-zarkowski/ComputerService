using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class DeviceValidator : AbstractValidator<Device>
{
    public DeviceValidator()
    {
        RuleFor(x => x.Name).NotNull()
            .MaximumLength(50);
        RuleFor(x => x.SerialNumber)
            .MaximumLength(50);
        RuleFor(x => x.SerialNumber)
            .MaximumLength(50);
        RuleFor(x => x.Condition)
            .MaximumLength(500);
        RuleFor(x => x.HasWarranty).NotNull();
        RuleFor(x => x.CustomerId).NotNull();
        RuleFor(x => x.OrderId).NotNull();
    }
}
