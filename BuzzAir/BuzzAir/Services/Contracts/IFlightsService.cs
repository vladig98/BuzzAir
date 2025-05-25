namespace BuzzAir.Services.Contracts
{
    public interface IFlightsService
    {
        Task<bool> Exists(string id);
        Task<bool> ExistsByOrigin(string origin);
        Task<bool> ExistsByDestination(string destination);
        Task<Flight?> GetById(string id);
        Task<Flight> GetByOrigin(string origin);
        Task<Flight> GetByDestination(string destination);
        Task<int> GetCountAsync();
        Task<List<SelectListItem>> GetAll();
        Task<List<Flight>> GetAllAsync(int pageSize, int pageNumber);
        Task<IEnumerable<Flight>> GetFlightsByOriginAndDestination(City origin, City destination, DateTime departure);
        Task<IEnumerable<Flight>> GetFlightsByCityId(string cityId);
        Task<IEnumerable<Flight>> GetFlightsForOriginIdAndDestinationId(string originId, string destinationId);
        Task Create(CreateFlightViewModel model);
        Task Update(FlightEditViewModel model);
        Task Delete(string flightId);
        List<FlightViewModel> GetFlightsDetails(ICollection<BookingFlight> flights);
        List<FlightViewModel> GetViewModels(IEnumerable<Flight> flights);
        Task<CreateFlightViewModel> CreateViewModel();
        Task<PaginatedList<Flight>> GetAllAsPaginatedListAsync(int pageSize, int? pageNumber);
        Task<FlightEditViewModel> GetEditViewModel(string flightId);
    }
}
