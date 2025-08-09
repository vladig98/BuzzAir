namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IAircraftService
{
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
    Task<Aircraft> GetAicraftModelByIdAsync(string id, CancellationToken token = default);
    Task<List<AircraftDTO>> GetAllAircraftAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
}
