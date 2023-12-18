using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IAirportService
    {
        Task<bool> Exists(string id);
        Task<bool> ExistsByName(string name);
        Task<Airport> GetById(string id);
        Task<Airport> GetByName(string name);
        Task<Airport> Create(string icao, string iata, string name, City city, State state, Country country, int elevation, double lat, double lgt, string tz);
        Task<IEnumerable<Airport>> GetAll();
    }
}
