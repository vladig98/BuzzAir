namespace BuzzAir.Areas.Admin.Repositories.Interfaces
{
    public interface IStateRepository
    {
        Task<List<State>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, bool includeCountry = false, CancellationToken token = default);
        Task<List<State>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, bool includeCountry = false, CancellationToken token = default);
        Task<bool> CanChangeLocationAsync(string id, CancellationToken token = default);
        Task CreateAsync(State state, CancellationToken token);
        Task DeleteAsync(string id, bool includeCountry = false, CancellationToken token = default);
        Task EditAsync(State state, CancellationToken token = default);
        Task<State> GetByIdAsync(string id, bool includeCountry = false, CancellationToken token = default);
        Task<long> GetCountAsync(CancellationToken token = default);
        Task<long> GetDeletedCountAsync(CancellationToken token = default);
        Task RestoreAsync(string id, bool includeCountry = false, CancellationToken token = default);
    }
}
