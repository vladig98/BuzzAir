using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAll();
        Task<City> GetById(string id);
        Task<City> GetByName(string name);
        Task<City> Create(Country country, string name, State? state = null);
    }
}
