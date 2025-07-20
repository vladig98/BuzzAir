namespace BuzzAir.Areas.Admin.Repositories.Interfaces
{
    public interface IAirportRepository
    {
        Task<List<Airport>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, AirportEnum includes = AirportEnum.None, CancellationToken token = default);
        Task<List<Airport>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, AirportEnum includes = AirportEnum.None, CancellationToken token = default);
        Task<bool> CanChangeLocationAsync(string id, CancellationToken token = default);
        Task CreateAsync(Airport airport, CancellationToken token = default);
        Task EditAsync(Airport airport, CancellationToken token = default);
        Task DeleteAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default);
        Task RestoreAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default);
        Task<Airport> GetByIdAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default);
        Task<Airport> GetDeletedByIdAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default);
        Task<long> GetCountAsync(CancellationToken token = default);
        Task<long> GetDeletedCountAsync(CancellationToken token = default);
    }
}
