namespace BuzzAir.Areas.Admin.Services;

public sealed class CountryService(BuzzAirDbContext dbContext) : ICountryService
{
    public Task<List<CountryDTO>> GetAllCountriesAsync(CancellationToken token = default)
    {
        return dbContext.Countries.Select(c => new CountryDTO(c.Id, c.Name, c.IsOfficiallyRecognizedCountry)).ToListAsync(token);
    }

    public async Task<Country> GetCountryBYIdAsync(string countryId, CancellationToken token)
    {
        Country country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == countryId, token)
            ?? throw new KeyNotFoundException($"No country with id {countryId} could be found.");

        return country;
    }
}
