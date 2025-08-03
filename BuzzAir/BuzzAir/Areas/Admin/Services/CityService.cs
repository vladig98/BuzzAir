namespace BuzzAir.Areas.Admin.Services;

public sealed class CityService(
    BuzzAirDbContext dbContext,
    ICountryService countryService,
    IStateService stateService,
    ITimezoneService timezoneService) : ICityService
{
    public async Task AddCityAsync(CreateCityVM model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        Country country = await countryService.GetCountryBYIdAsync(model.CountryId, token);
        State? state = await stateService.GetStateByIdAsync(model.StateId, token);
        Timezone timezone = await timezoneService.GetTimezoneByIdAsync(model.TimezoneId, token);

        City city = new()
        {
            Name = model.Name,
            Country = country,
            State = state,
            Timezone = timezone
        };

        _ = await dbContext.Cities.AddAsync(city, token);
        _ = await dbContext.SaveChangesAsync(token);
    }
}
