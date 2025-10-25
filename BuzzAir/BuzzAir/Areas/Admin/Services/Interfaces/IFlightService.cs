namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IFlightService
{
    Task AddFlightAsync(CreateFlightVM viewModel, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task<List<FlightDTO>> GetAllDeletedFlightsAsync(int pageNumber, int itemsPerPage, CancellationToken token = default);
    Task<List<FlightDTO>> GetAllFlightsAsync(int pageNumber, int itemsPerPage, CancellationToken token = default);
    Task<int> GetCountAsync(CancellationToken token = default);
    Task<int> GetDeletedCountAsync(CancellationToken token = default);
    Task<FlightDTO> GetDeletedFlightByIdAsync(string id, CancellationToken token = default);
    Task<FlightDTO> GetFlightByIdAsync(string id, CancellationToken token = default);
    Task<Flight?> GetFlightModelByIdAsync(string? flightId, CancellationToken token);
    Task<IList<FlightDTO>> GetFlightsByAirportsAndDatesAsync(string originId, string destinationId, DateTime departureDate, CancellationToken token = default);
    Task<Dictionary<string, DateTime>> GetFlightsDatesBasedOnOriginAndDestination(string originId, string destinationId, CancellationToken token = default);
    Task<Dictionary<string, Dictionary<string, string>>> GetFutureFlightsDestinationsBasedOnOriginAsync(string originId, int pageIndex, int itemsPerPage, string keyword, CancellationToken token = default);
    Task<Dictionary<string, Dictionary<string, string>>> GetFutureFlightsOriginsAsync(int pageIndex, int itemsPerPage, string keyword, CancellationToken token = default);
    Task<Dictionary<string, DateTime>> GetReturnFlightsDatesBasedOnOriginAndDestination(string originId, string destinationId, DateTime earliest, CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
    Task UpdateFlightAsync(EditFlightVM viewModel, CancellationToken token = default);
}
