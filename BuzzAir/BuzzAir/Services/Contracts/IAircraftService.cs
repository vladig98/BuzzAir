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
        Task<List<Aircraft>> GetAllAsQueryable(int pageSize, int? pageNumber);
        Task<int> GetCount();
        Task<Aircraft> Edit(string id, string name, int numberOFSeats);
        Task<bool> CanChangeSeats(string id, int numberOfSeats);
        Task<Aircraft> Delete(string id);
    }
}
