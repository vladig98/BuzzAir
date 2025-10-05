namespace BuzzAir.Hubs;

public class FlightHub(IFlightService flightService) : Hub
{
    public Task<Dictionary<string, Dictionary<string, string>>> GetOrigins(int pageIndex, int itemsPerPage)
    {
        return flightService.GetFutureFlightsOriginsAsync(pageIndex, itemsPerPage, Context.ConnectionAborted);
    }

    public Task<Dictionary<string, Dictionary<string, string>>> GetDestinations(string originId)
    {
        return flightService.GetFutureFlightsDestinationsBasedOnOriginAsync(originId, Context.ConnectionAborted);
    }

    public Task<Dictionary<string, DateTime>> GetAvailableDates(string originId, string destinationId)
    {
        return flightService.GetFlightsDatesBasedOnOriginAndDestination(originId, destinationId, Context.ConnectionAborted);
    }

    public Task<Dictionary<string, DateTime>> GetAvailableReturnDates(string originId, string destinationId, string originDateISO)
    {
        if (string.IsNullOrWhiteSpace(originDateISO))
        {
            return flightService.GetFlightsDatesBasedOnOriginAndDestination(originId, destinationId, Context.ConnectionAborted);
        }

        if (!DateTime.TryParseExact(
                originDateISO,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out DateTime earliest))
        {
            earliest = DateTime.UtcNow;
        }

        return flightService.GetReturnFlightsDatesBasedOnOriginAndDestination(originId, destinationId, earliest, Context.ConnectionAborted);
    }
}
