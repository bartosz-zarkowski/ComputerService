using ComputerService.Entities;
using FluentValidation;

namespace ComputerService.Validators;

public class UserTrackingValidator : AbstractValidator<UserTracking>
{
    public UserTrackingValidator()
    {
        RuleFor(x => x.FirstName).NotNull()
            .MaximumLength(50);
        RuleFor(x => x.LastName).NotNull()
            .MaximumLength(50);
        RuleFor(x => x.TrackingActionType).IsInEnum();
        RuleFor(x => x.TrackingActionType).NotNull();
        RuleFor(x => x.Date).NotNull();
    }
}