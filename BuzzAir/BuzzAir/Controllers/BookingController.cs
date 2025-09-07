namespace BuzzAir.Controllers;

public class BookingController(IFlightService flightService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create(string originId, string destinationId, DateTime departureDate, DateTime? returnDate, CancellationToken token)
    {
        CreateBookingDto dto = new();

        IList<FlightDTO> outboundFlights = await flightService.GetFlightsByAirportsAndDatesAsync(originId, destinationId, departureDate, token);
        dto.AddOutboundFlight(outboundFlights);

        if (returnDate.HasValue)
        {
            IList<FlightDTO> inboundFlights = await flightService.GetFlightsByAirportsAndDatesAsync(destinationId, originId, returnDate.Value, token);
            dto.AddInboundFlight(inboundFlights);
        }

        return View(dto);
    }
}
