namespace BuzzAir.DataValidators.CityValidators;

public class CreateCityVMValidator : AbstractValidator<CreateCityVM>
{
    private readonly ICountryService _countryService;
    private readonly IStateService _stateService;
    private readonly ITimezoneService _timezoneService;

    public CreateCityVMValidator(
        ICountryService countryService,
        IStateService stateService,
        ITimezoneService timezoneService)
    {
        _countryService = countryService;
        _stateService = stateService;
        _timezoneService = timezoneService;

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

        // StateId: optional, but if provided must be in the list of states
        _ = RuleFor(x => x.StateId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (id, ct) => string.IsNullOrEmpty(id) || await _stateService.ExistsAsync(id, ct))
            .WithMessage("Selected state is not valid.");

        // TimezoneId: required, must be in the provided list
        _ = RuleFor(x => x.TimezoneId)
            .NotEmpty()
            .WithMessage("Please select a timezone.")
            .MustAsync(async (id, ct) => await _timezoneService.ExistsAsync(id, ct))
            .WithMessage("Selected timezone is not valid.");
    }
}
