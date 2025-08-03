
namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IStateService
{
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<State?> GetStateByIdAsync(string? stateId, CancellationToken token);
    Task<List<StateDTO>> GetStatesByCountryAsync(string countryId, CancellationToken token = default);
}
