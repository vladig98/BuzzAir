namespace BuzzAir.Services.Interfaces
{
    public interface IAirportService
    {
        Task<Airport> GetByIdAsync(string id);
        Task CreateAsync(AirportCreateViewModel model);
        Task<IEnumerable<Airport>> GetAll();
        Task<IEnumerable<Airport>> GetAllForCountry(string countryId);
        Task<AirportViewModel> GetEditViewModelAsync(string airportId);
        Task<int> GetCount();
        Task<PaginatedList<Airport>> GetAllAsync(int pageSize, int? pageNumber);
        Task<AirportCreateViewModel> GetCreateViewModelAsync();
    }
}
