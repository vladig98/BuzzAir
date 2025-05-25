
namespace BuzzAir.Services.Contracts
{
    public interface IAirportService
    {
        Task<bool> Exists(string id);
        Task<bool> ExistsByName(string name);
        Task<Airport> GetByIdAsync(string id);
        Task<Airport> GetByName(string name);
        Task CreateAsync(AirportCreateViewModel model);
        Task<IEnumerable<Airport>> GetAll();
        Task<IEnumerable<Airport>> GetAllForCountry(string countryId);
        Task<AirportViewModel> GetEditViewModelAsync(string airportId);
        Task<int> GetCount();
        Task<PaginatedList<Airport>> GetAllAsync(int pageSize, int? pageNumber);
        Task<AirportCreateViewModel> GetCreateViewModelAsync();
    }
}
