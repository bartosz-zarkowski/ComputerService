using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class OrderDetailsValidator : AbstractValidator<OrderDetails>
{
    public OrderDetailsValidator()
    {
        RuleFor(x => x.Id).NotNull();

        RuleFor(x => x.ServiceDescription).MaximumLength(500);
        RuleFor(x => x.AdditionalInformation).MaximumLength(500);
        RuleFor(x => x.HardwareCharges).ScalePrecision(2, 10);
        RuleFor(x => x.ServiceCharges).ScalePrecision(2, 10);
    }
}
