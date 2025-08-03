
namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IStateService
{
    Task<State?> GetStateByIdAsync(string? stateId, CancellationToken token);
    Task<List<StateDTO>> GetStatesByCountryAsync(string countryId, CancellationToken token = default);
}
