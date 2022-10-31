using DF.Ordering.Models;
using FluentValidation;

namespace ComputerService.Validators;

public class ParametersValidator : AbstractValidator<ParametersModel>
{
    public ParametersValidator()
    {
        int[] pageSizes = { 10, 25, 50 };
        RuleFor(o => o.PageSize)
            .Must(x => pageSizes.Contains(x))
            .WithMessage("Page Size must be in [" + String.Join(", ", pageSizes) + "].");
        RuleFor(o => o.PageNumber).GreaterThan(0);
    }
}