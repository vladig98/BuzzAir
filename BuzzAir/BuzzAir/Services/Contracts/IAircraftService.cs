using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IAircraftService
    {
        Task<bool> ExistById(string id);
        Task<bool> ExistsByName(string name);
        Task<Aircraft> GetById(string id);
        Task<Aircraft> GetByName(string name);
        Task<IEnumerable<Aircraft>> GetAll();
        Task<Aircraft> Create(string name, int numberOFSeats);
    }
}
