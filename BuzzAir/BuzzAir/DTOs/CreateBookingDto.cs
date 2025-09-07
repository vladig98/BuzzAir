namespace BuzzAir.DTOs;

public class CreateBookingDto
{
    public IList<FlightDTO> OutboundFlights { get; } = [];
    public IList<FlightDTO> InboundFlights { get; } = [];
    public IList<ServiceDto> Services { get; } = [];
    public int PassengersCount { get; set; } = 1;
    public string OutboundId { get; set; } = string.Empty;
    public string? InboundId { get; set; }
    public IList<PassengerDto> Passengers { get; } = [];
    public Dictionary<int, List<string>> PassengerServices { get; } = [];
    public PaymentDto Payment { get; set; } = new();

    public void AddOutboundFlight(IList<FlightDTO> outboundFlights)
    {
        if (outboundFlights is null)
        {
            return;
        }

        foreach (FlightDTO flight in outboundFlights)
        {
            OutboundFlights.Add(flight);
        }
    }

    public void AddInboundFlight(IList<FlightDTO> inboundFlights)
    {
        if (inboundFlights is null)
        {
            return;
        }

        foreach (FlightDTO flight in inboundFlights)
        {
            InboundFlights.Add(flight);
        }
    }
}
