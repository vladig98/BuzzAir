namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IAirportService
{
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
    Task<Airport> GetAirportModelByIdAsync(string id, CancellationToken token = default);
    Task<List<AirportDTO>> GetAllAirportsAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
}
