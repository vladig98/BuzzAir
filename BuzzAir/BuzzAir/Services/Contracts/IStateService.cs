namespace BuzzAir.Services.Contracts
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetAllAsync();
        Task<State?> GetByIdAsync(string id);
    }
}
