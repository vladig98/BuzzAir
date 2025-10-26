namespace BuzzAir.Hubs;

public class SearchHub(IFlightService flightService, ICityService cityService) : Hub
{
    public Task<Dictionary<string, Dictionary<string, string>>> GetOrigins(int pageIndex, int itemsPerPage, string currentSearch)
    {
        return flightService.GetFutureFlightsOriginsAsync(pageIndex, itemsPerPage, currentSearch, Context.ConnectionAborted);
    }

    public Task<Dictionary<string, Dictionary<string, string>>> GetDestinations(string originId, int pageIndex, int itemsPerPage, string currentSearch)
    {
        return flightService.GetFutureFlightsDestinationsBasedOnOriginAsync(originId, pageIndex, itemsPerPage, currentSearch, Context.ConnectionAborted);
    }

    public Task<Dictionary<string, Dictionary<string, string>>> GetCities(int pageIndex, int itemsPerPage, string currentSearch)
    {
        return cityService.GetAllCitiiesPaginatedAsync(pageIndex, itemsPerPage, currentSearch, Context.ConnectionAborted);
    }
}
