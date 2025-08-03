namespace BuzzAir.DataValidators;

public class DeleteCityVMValidator : AbstractValidator<DeleteCityVM>
{
    public DeleteCityVMValidator()
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

        // StateName (optional, but if provided must not be all whitespace)
        _ = RuleFor(x => x.StateName)
            .Must(s => string.IsNullOrWhiteSpace(s) || !string.IsNullOrWhiteSpace(s.Trim()))
            .WithMessage("State name, when provided, cannot be blank.");

        // TimezoneName
        _ = RuleFor(x => x.TimezoneName)
            .NotEmpty()
            .WithMessage("Timezone is required.")
            .MaximumLength(100)
            .WithMessage("Timezone name must be at most 100 characters.");
    }
}
