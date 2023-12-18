using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAll();
        Task<Country> GetById(string id);
    }
}
