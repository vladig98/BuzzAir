namespace BuzzAir.DataValidators.FlightValidators;

public class CreateFlightVMValidator : AbstractValidator<CreateFlightVM>
{
    private readonly IAirportService _airportService;
    private readonly IAircraftService _aircraftService;

    public CreateFlightVMValidator(
        IAirportService airportService,
        IAircraftService aircraftService)
    {
        _airportService = airportService;
        _aircraftService = aircraftService;

        _ = RuleFor(x => x.FlightNumber)
                .NotEmpty().WithMessage("Flight number is required.")
                .MaximumLength(10).WithMessage("Flight number must be at most 10 characters.");

        _ = RuleFor(x => x.OriginId)
                .NotEmpty().WithMessage("Origin airport is required.")
                .MustAsync(async (id, ct) => await _airportService.ExistsAsync(id, ct))
                .WithMessage("Selected origin airport is not valid.");

        _ = RuleFor(x => x.DestinationId)
                .NotEmpty().WithMessage("Destination airport is required.")
                .MustAsync(async (id, ct) => await _airportService.ExistsAsync(id, ct))
                .WithMessage("Selected destination airport is not valid.");

        // origin != destination
        _ = RuleFor(x => x)
                .Must(x => !string.Equals(x.OriginId, x.DestinationId, StringComparison.OrdinalIgnoreCase))
                .WithMessage("Origin and destination cannot be the same.");

        _ = RuleFor(x => x.AircraftId)
                .NotEmpty().WithMessage("Aircraft is required.")
                .MustAsync(async (id, ct) => await _aircraftService.ExistsAsync(id, ct))
                .WithMessage("Selected aircraft is not valid.");

        _ = RuleFor(x => x.ArrivalUTC)
                .GreaterThan(x => x.DepartureUTC)
                .WithMessage("Arrival time must be after departure time.")
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Arrival time must be in the future.");

        _ = RuleFor(x => x.DepartureUTC)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Departure time must be in the future.");

        _ = RuleFor(x => x.PriceInEur)
                .GreaterThan(0m).WithMessage("Price must be greater than 0.");
    }
}
