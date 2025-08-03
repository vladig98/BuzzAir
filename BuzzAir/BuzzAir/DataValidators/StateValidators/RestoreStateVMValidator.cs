namespace BuzzAir.DataValidators.StateValidators;

public class RestoreStateVMValidator : AbstractValidator<RestoreStateVM>
{
    public RestoreStateVMValidator()
    {
        // Id is required (hidden field, but must round-trip)
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Invalid city identifier.");

        // Name
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("City name is required.")
            .MaximumLength(100)
            .WithMessage("City name must be at most 100 characters.");

        // CountryName
        _ = RuleFor(x => x.CountryName)
            .NotEmpty()
            .WithMessage("Country is required.")
            .MaximumLength(100)
            .WithMessage("Country name must be at most 100 characters.");
    }
}
