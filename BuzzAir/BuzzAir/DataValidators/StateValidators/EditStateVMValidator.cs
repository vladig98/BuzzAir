namespace BuzzAir.DataValidators.StateValidators;

public class EditStateVMValidator : AbstractValidator<EditStateVM>
{
    private readonly ICountryService _countryService;

    public EditStateVMValidator(ICountryService countryService)
    {
        _countryService = countryService;

        // Name: required, max length 100
        _ = RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("City name is required.")
            .MaximumLength(100)
            .WithMessage("City name must be at most 100 characters.");

        // CountryId: required, must be in the provided list
        _ = RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithMessage("Please select a country.")
            .MustAsync(async (id, ct) => await _countryService.ExistsAsync(id, ct))
            .WithMessage("Selected country is not valid.");

        // Id shouldn't be empty
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("City Id is required.");
    }
}
