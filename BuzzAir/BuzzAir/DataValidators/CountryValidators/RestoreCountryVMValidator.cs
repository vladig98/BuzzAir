namespace BuzzAir.DataValidators.CountryValidators;

public class RestoreCountryVMValidator : AbstractValidator<RestoreCountryVM>
{
    public RestoreCountryVMValidator()
    {
        // Name: required, max length 100
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Country name is required.")
            .MaximumLength(100)
            .WithMessage("Country name must be at most 100 characters.");

        // ISO: required, exactly 2 uppercase letters
        _ = RuleFor(x => x.ISO2)
            .NotEmpty()
            .WithMessage("ISO2 code is required.")
            .Length(2)
            .WithMessage("ISO2 code must be exactly 2 characters.")
            .Matches("^[A-Z]{2}$")
            .WithMessage("ISO2 code must be two uppercase letters.");

        _ = RuleFor(x => x.ISO3)
             .NotEmpty()
             .WithMessage("ISO3 code is required.")
             .Length(3)
             .WithMessage("ISO3 code must be exactly 3 characters.")
             .Matches("^[A-Z]{3}$")
             .WithMessage("ISO3 code must be three uppercase letters.");

        // Id shouldn't be empty
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("City Id is required.");
    }
}
