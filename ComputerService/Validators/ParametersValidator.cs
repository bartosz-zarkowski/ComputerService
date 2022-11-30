using ComputerService.Models;
using FluentValidation;

namespace ComputerService.Validators;

public class ParametersValidator : AbstractValidator<ParametersModel>
{
    public ParametersValidator()
    {
        int[] pageSizes = { 5, 10, 25 };
        RuleFor(o => o.PageSize)
            .Must(x => pageSizes.Contains(x))
            .WithMessage("Page Size must be in [" + String.Join(", ", pageSizes) + "].");
        RuleFor(o => o.PageNumber).GreaterThan(0);
    }
}