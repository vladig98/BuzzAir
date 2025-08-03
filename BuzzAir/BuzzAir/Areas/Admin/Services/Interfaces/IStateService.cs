namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IStateService
{
    Task AddStateAsync(CreateStateVM viewModel, CancellationToken token = default);
    Task<int> CountAsync(CancellationToken token = default);
    Task<int> CountDeletedAsync(CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<List<StateDTO>> GetAllDeletedStatesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default);
    Task<List<StateDTO>> GetAllStatesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default);
    Task<State?> GetStateModelByIdAsync(string? stateId, CancellationToken token = default);
    Task<StateDTO> GetStateByIdAsync(string stateId, CancellationToken token = default);
    Task<List<StateDTO>> GetStatesByCountryAsync(string countryId, CancellationToken token = default);
    Task UpdateStateAsync(EditStateVM viewModel, CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task<StateDTO> GetDeletedStateByIdAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
}
