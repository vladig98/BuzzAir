namespace BuzzAir.DataValidators.FlightValidators;

public class DeleteFlightVMValidator : AbstractValidator<DeleteFlightVM>
{
    private readonly IAirportService _airportService;
    private readonly IAircraftService _aircraftService;

    public DeleteFlightVMValidator(
        IAirportService airportService,
        IAircraftService aircraftService)
    {
        _airportService = airportService;
        _aircraftService = aircraftService;

        _ = RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");

        _ = RuleFor(x => x.FlightNumber)
                .NotEmpty().WithMessage("Flight number is required.")
                .MaximumLength(10).WithMessage("Flight number must be at most 10 characters.");

        _ = RuleFor(x => x.OriginName)
                .NotEmpty().WithMessage("Origin airport is required.")
                .MustAsync(async (name, ct) => await _airportService.ExistsByNameAsync(name, ct))
                .WithMessage("Selected origin airport is not valid.");

        _ = RuleFor(x => x.DestinationName)
                .NotEmpty().WithMessage("Destination airport is required.")
                .MustAsync(async (name, ct) => await _airportService.ExistsByNameAsync(name, ct))
                .WithMessage("Selected destination airport is not valid.");

        _ = RuleFor(x => x.AircraftModel)
                .NotEmpty().WithMessage("Aircraft is required.")
                .MustAsync(async (name, ct) => await _aircraftService.ExistsByNameAsync(name, ct))
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
