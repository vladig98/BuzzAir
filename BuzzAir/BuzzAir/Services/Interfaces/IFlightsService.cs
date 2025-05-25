namespace BuzzAir.Services.Interfaces
{
    public interface IFlightsService
    {
        Task<Flight?> GetById(string id);
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
