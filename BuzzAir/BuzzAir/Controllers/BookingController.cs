namespace BuzzAir.Controllers;

public class BookingController(
    IFlightService flightService,
    IServicesService servicesService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create(
        string originId,
        string destinationId,
        DateTime departureDate,
        DateTime? returnDate,
        int passengersCount,
        CancellationToken token)
    {
        IList<FlightDTO> outboundFlights = await flightService.GetFlightsByAirportsAndDatesAsync(originId, destinationId, departureDate, token);
        IList<FlightDTO> inboundFlights = [];
        IList<ServiceDto> services = await servicesService.GetServicesAsync(token);
        IList<ServiceDto> baggage = await servicesService.GetBaggageServicesAsync(token);
        IList<ServiceDto> seats = await servicesService.GetSeatServicesAsync(token);

        if (returnDate.HasValue)
        {
            inboundFlights = await flightService.GetFlightsByAirportsAndDatesAsync(destinationId, originId, returnDate.Value, token);
        }

        CreateBookingDto dto = new();

        dto.AddOutboundFlight(outboundFlights);
        dto.AddInboundFlight(inboundFlights);
        dto.AddPassengers(passengersCount);
        dto.AddServices(services);
        dto.AddBaggage(baggage);
        dto.AddSeats(seats);

        return View(dto);
    }

    [HttpPost]
    public Task<IActionResult> CreateBooking(CreateBookingDto data, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
