namespace BuzzAir.Areas.Admin.Services;

public sealed class CountryService(BuzzAirDbContext dbContext) : ICountryService
{
    public Task<List<CountryDTO>> GetAllCountriesAsync(CancellationToken token = default)
    {
        return dbContext.Countries.Select(c => new CountryDTO(c.Id, c.Name, c.IsOfficiallyRecognizedCountry)).ToListAsync(token);
    }
}
