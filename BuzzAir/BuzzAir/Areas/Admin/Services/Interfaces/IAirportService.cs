namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IAirportService
{
    Task AddAirportAsync(CreateAirportVM viewModel, CancellationToken token = default);
    Task<int> CountAsync(CancellationToken token = default);
    Task<int> CountDeletedAsync(CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<bool> ExistsByIATAAsync(string iata, string? id = null, CancellationToken token = default);
    Task<bool> ExistsByICAOAsync(string icao, string? id = null, CancellationToken token = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
    Task<AirportDTO> GetAirportByIdAsync(string id, CancellationToken token = default);
    Task<Airport> GetAirportModelByIdAsync(string id, CancellationToken token = default);
    Task<List<AirportDTO>> GetAllAirportsAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<List<AirportDTO>> GetAllDeletedAirportsAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<AirportDTO> GetDeletedAirportByIdAsync(string id, CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
    Task UpdateAirportAsync(EditAirportVM viewModel, CancellationToken token = default);
}
