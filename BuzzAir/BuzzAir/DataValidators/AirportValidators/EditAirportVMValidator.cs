namespace BuzzAir.DataValidators.AirportValidators;

public partial class EditAirportVMValidator : AbstractValidator<EditAirportVM>
{
    private readonly ICityService _cityService;
    private readonly IAirportService _airportService;

    public EditAirportVMValidator(ICityService cityService, IAirportService airportService)
    {
        _cityService = cityService;
        _airportService = airportService;

        // ICAO: required, length 4, letters only (validate trimmed/upper)
        _ = RuleFor(x => x.ICAO)
                .Cascade(CascadeMode.Stop)
                .Must(icao => !string.IsNullOrWhiteSpace(icao))
                .WithMessage("ICAO code is required.")
                .Must(icao =>
                {
                    if (string.IsNullOrWhiteSpace(icao))
                    {
                        return true;
                    }

                    string icaoTrimmed = icao.Trim().ToUpperInvariant();
                    return icaoTrimmed.Length == 4 && ICAORagex().IsMatch(icaoTrimmed);
                })
                .WithMessage("ICAO code must be 4 letters (A-Z).");

        _ = RuleFor(x => x.ICAO)
                .MustAsync(async (vm, icao, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(icao))
                    {
                        return true;
                    }

                    string normalized = icao.Trim().ToUpperInvariant();
                    return !await _airportService.ExistsByICAOAsync(normalized, vm.Id, ct);
                })
                .WithMessage("An airport with this ICAO code already exists.");

        // IATA: required, length 3, alphanumeric (validate trimmed/upper)
        _ = RuleFor(x => x.IATA)
                .Cascade(CascadeMode.Stop)
                .Must(iata => !string.IsNullOrWhiteSpace(iata))
                .WithMessage("IATA code is required.")
                .Must(iata =>
                {
                    if (string.IsNullOrWhiteSpace(iata))
                    {
                        return true;
                    }

                    string iataTrimmed = iata.Trim().ToUpperInvariant();
                    return iataTrimmed.Length == 3 && IATARegex().IsMatch(iataTrimmed);
                })
                .WithMessage("IATA code must be 3 letters or numbers (A-Z, 0-9).");

        _ = RuleFor(x => x.IATA)
                .MustAsync(async (vm, iata, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(iata))
                    {
                        return true;
                    }

                    string normalized = iata.Trim().ToUpperInvariant();
                    return !await _airportService.ExistsByIATAAsync(normalized, vm.Id, ct);
                })
                .WithMessage("An airport with this IATA code already exists.");

        // Name: required, max length 150 (check trimmed length)
        _ = RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Airport name is required.")
                .Must(name => (name?.Trim().Length ?? 0) <= 150)
                .WithMessage("Airport name must be at most 150 characters.");

        // Latitude: optional, if present must be between -90 and 90
        _ = RuleFor(x => x.Latitude)
                .Must(lat => !lat.HasValue || (lat.Value >= -90m && lat.Value <= 90m))
                .WithMessage("Latitude must be between -90 and 90 degrees.");

        // Longitude: optional, if present must be between -180 and 180
        _ = RuleFor(x => x.Longitude)
                .Must(lon => !lon.HasValue || (lon.Value >= -180m && lon.Value <= 180m))
                .WithMessage("Longitude must be between -180 and 180 degrees.");

        // Elevation: optional, sensible bounds
        _ = RuleFor(x => x.ElevationAboveSeaLevel)
                .Must(elev => !elev.HasValue || (elev.Value >= -500 && elev.Value <= 10000))
                .WithMessage("Elevation must be between -500 and 10,000 meters.");

        // CityId: required and must exist
        _ = RuleFor(x => x.CityId)
                .Must(id => !string.IsNullOrWhiteSpace(id))
                .WithMessage("City selection is required.");


        _ = RuleFor(x => x.CityId)
                .MustAsync(async (cityId, ct) => string.IsNullOrWhiteSpace(cityId) || await _cityService.ExistsByIdAsync(cityId.Trim(), ct))
                .WithMessage("Selected city does not exist.");

        // Id
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Invalid city identifier.");
    }

    [GeneratedRegex("^[A-Z]{4}$", RegexOptions.Compiled)]
    private static partial Regex ICAORagex();
    [GeneratedRegex("^[A-Z0-9]{3}$", RegexOptions.Compiled)]
    private static partial Regex IATARegex();
}
