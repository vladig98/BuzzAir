
namespace BuzzAir.Areas.Admin.Services.Interfaces
{
    public interface IFlightsService
    {
        Task<PaginatedList<FlightDTO>> AllAsync(int? pageNumber, CancellationToken token);
        Task<PaginatedList<FlightDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token);
        Task CreateAsync(CreateFlightVM model, CancellationToken token);
        Task DeleteAsync(string id, CancellationToken token);
        Task EditAsync(EditFlightVM model, CancellationToken token);
        Task<List<SelectListItem>> GetAll();
        Task<Flight?> GetById(string v);
        Task<DeleteFlightVM> GetDeleteDetailsAsync(string id, CancellationToken token);
        Task<EditFlightVM> GetEditDetailsAsync(string id, CancellationToken token);
        Task<IEnumerable<Flight>> GetFlightsByCityId(string cityId);
        Task<IEnumerable<Flight>> GetFlightsByOriginAndDestination(City origin, City destination, DateTime departure);
        List<FlightViewModel> GetFlightsDetails(ICollection<BookingFlight> flights);
        Task<IEnumerable<Flight>> GetFlightsForOriginIdAndDestinationId(string originId, string destinationId);
        Task<RestoreFlightVM> GetRestoreDetailsAsync(string id, CancellationToken token);
        List<FlightViewModel> GetViewModels(IEnumerable<Flight> outboundFlights);
        Task RestoreAsync(string id, CancellationToken token);
    }
}
