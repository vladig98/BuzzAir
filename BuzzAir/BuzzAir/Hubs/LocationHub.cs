namespace BuzzAir.Hubs;

public sealed class LocationHub(
    IStateService stateService,
    ICityService cityService) : Hub
{
    public Task<List<StateDTO>> GetStatesByCountry(string countryId)
    {
        return stateService.GetStatesByCountryAsync(countryId);
    }

    public Task<List<CityDTO>> GetCitiesByStateAndCountry(string? stateId, string countryId)
    {
        return cityService.GetCitiesByStateAndCountryAsync(stateId, countryId);
    }
}
