namespace BuzzAir.Areas.Admin.Repositories.Interfaces
{
    public interface IFlightsRepository
    {
        Task<List<Flight>> AllAsync(int? pageNumber, int? itemPerPage, CancellationToken ct);
        Task<List<Flight>> AllDeletedAsync(int? pageNumber, int? itemPerPage, CancellationToken ct);
        Task CreateAsync(Flight flight, CancellationToken token);
        Task<Flight> GetByIdAsync(string id, CancellationToken ct);
        Task<long> GetCountAsync(CancellationToken token);
        Task<Flight> GetDeletedByIdAsync(string id, CancellationToken ct);
        Task<long> GetDeletedCountAsync(CancellationToken token);
    }
}
