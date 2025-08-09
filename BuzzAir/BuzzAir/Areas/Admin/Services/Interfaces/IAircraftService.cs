namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IAircraftService
{
    Task AddAircraftAsync(CreateAircraftVM viewModel, CancellationToken token = default);
    Task<int> CountAsync(CancellationToken token = default);
    Task<int> CountDeletedAsync(CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
    Task<Aircraft> GetAicraftModelByIdAsync(string id, CancellationToken token = default);
    Task<AircraftDTO> GetAircraftByIdAsync(string id, CancellationToken token = default);
    Task<List<AircraftDTO>> GetAllAircraftAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<List<AircraftDTO>> GetAllDeletedAircraftAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<AircraftDTO> GetDeletedAircraftByIdAsync(string id, CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
    Task UpdateAircraftAsync(EditAircraftVM viewModel, CancellationToken token = default);
}
