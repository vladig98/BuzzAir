namespace BuzzAir.Areas.Admin.Repositories.Interfaces
{
    public interface IAircraftRepository
    {
        Task CreateAsync(Aircraft aircraft, CancellationToken token = default);
        Task EditAsync(Aircraft aircraft, CancellationToken token = default);
        Task DeleteAsync(string id, CancellationToken token = default);
        Task RestoreAsync(string id, CancellationToken token = default);
        Task<bool> CanChangeSeatsAsync(string id, int newSeatsNumber, CancellationToken token = default);
        Task<Aircraft> GetByIdAsync(string id, CancellationToken token = default);
        Task<Aircraft> GetDeletedByIdAsync(string id, CancellationToken token = default);
        Task<List<Aircraft>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default);
        Task<List<Aircraft>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default);
        Task<long> GetCountAsync(CancellationToken token = default);
        Task<long> GetDeletedCountAsync(CancellationToken token = default);
    }
}
