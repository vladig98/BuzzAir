namespace BuzzAir.Services.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetAllAsync();
        Task<State?> GetByIdAsync(string id);
    }
}
