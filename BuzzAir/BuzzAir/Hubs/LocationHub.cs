namespace BuzzAir.Hubs;

public sealed class LocationHub(IStateService stateService) : Hub
{
    public Task<List<StateDTO>> GetStatesByCountry(string countryId)
    {
        return stateService.GetStatesByCountryAsync(countryId);
    }
}
