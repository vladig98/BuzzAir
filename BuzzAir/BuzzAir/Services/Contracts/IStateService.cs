using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetAll();
        Task<State> GetById(string id);
    }
}
