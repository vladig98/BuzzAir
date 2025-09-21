namespace BuzzAir.DTOs;

public class CreateBookingDto
{
    public IList<FlightDTO> OutboundFlights { get; } = [];
    public IList<FlightDTO> InboundFlights { get; } = [];
    public IList<ServiceDto> Services { get; } = [];
    public IList<ServiceDto> BaggageService { get; } = [];
    public IList<ServiceDto> SeatsServices { get; } = [];
    public IList<SelectListItem> CountriesSelect { get; } = [];
    public string OutboundId { get; set; } = string.Empty;
    public string? InboundId { get; set; }
    public IList<PassengerDto> Passengers { get; } = [];
    public PaymentDto Payment { get; set; } = new();

    public void AddServices(IList<ServiceDto> services)
    {
        if (services is null)
        {
            return;
        }

        foreach (ServiceDto service in services)
        {
            Services.Add(service);
        }
    }

    public void AddSeats(IList<ServiceDto> seats)
    {
        if (seats is null)
        {
            return;
        }

        foreach (ServiceDto seat in seats)
        {
            SeatsServices.Add(seat);
        }
    }

    public void AddBaggage(IList<ServiceDto> baggage)
    {
        if (baggage is null)
        {
            return;
        }

        foreach (ServiceDto bag in baggage)
        {
            BaggageService.Add(bag);
        }
    }

    public void AddPassengers(int passengersCount)
    {
        for (int i = 0; i < passengersCount; i++)
        {
            Passengers.Add(new PassengerDto());
        }
    }

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

    public void AddCountries(IList<CountryDTO> countries)
    {
        if (countries is null)
        {
            return;
        }

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as countries" };

        foreach (CountryDTO country in countries)
        {
            CountriesSelect.Add(new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id,
                Group = country.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup
            });
        }
    }
}
