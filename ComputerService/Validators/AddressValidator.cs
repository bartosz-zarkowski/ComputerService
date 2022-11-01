using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Country).NotNull()
            .MaximumLength(90);
        RuleFor(x => x.State).NotNull()
            .MaximumLength(90);
        RuleFor(x => x.City).NotNull()
            .MaximumLength(90);
        RuleFor(x => x.PostalCode).NotNull()
            .MaximumLength(18);
        RuleFor(x => x.Street).NotNull()
            .MaximumLength(90);
        RuleFor(x => x.StreetNumber).NotNull()
            .MaximumLength(90);
        RuleFor(x => x.Apartment).NotNull()
            .MaximumLength(90);
    }
}
