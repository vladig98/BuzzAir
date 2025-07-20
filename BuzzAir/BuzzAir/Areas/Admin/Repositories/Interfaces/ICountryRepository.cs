namespace BuzzAir.Areas.Admin.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default);
        Task<List<Country>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default);
        Task CreateAsync(Country country, CancellationToken token = default);
        Task DeleteAsync(string id, CancellationToken token = default);
        Task EditAsync(Country country, CancellationToken token = default);
        Task<Country> GetByIdAsync(string id, CancellationToken token = default);
        Task<long> GetCountAsync(CancellationToken token = default);
        Task<long> GetDeletedCountAsync(CancellationToken token = default);
        Task RestoreAsync(string id, CancellationToken token = default);
    }
}
