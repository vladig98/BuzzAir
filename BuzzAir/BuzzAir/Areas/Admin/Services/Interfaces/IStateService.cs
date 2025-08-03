namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface IStateService
{
    Task<List<StateDTO>> GetStatesByCountryAsync(string countryId, CancellationToken token = default);
}
