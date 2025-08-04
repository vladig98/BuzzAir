namespace BuzzAir.DataValidators.CountryValidators;

public class CreateCountryVMValidator : AbstractValidator<CreateCountryVM>
{
    public CreateCountryVMValidator()
    {
        // Name: required, max length 100
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Country name is required.")
            .MaximumLength(100)
            .WithMessage("Country name must be at most 100 characters.");

        // ISO: required, exactly 2 uppercase letters
        _ = RuleFor(x => x.ISO)
            .NotEmpty()
            .WithMessage("ISO code is required.")
            .Length(2)
            .WithMessage("ISO code must be exactly 2 characters.")
            .Matches("^[A-Z]{2}$")
            .WithMessage("ISO code must be two uppercase letters.");
    }
}
