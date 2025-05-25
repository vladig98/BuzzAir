namespace BuzzAir.Services.Contracts
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(string id);
        Task<City> GetByNameAsync(string name);
        Task<City> CreateAsync(string name, string countyId, string stateId);
    }
}
