namespace BuzzAir.Areas.Admin.Services;

public sealed class CountryService(BuzzAirDbContext dbContext) : ICountryService
{
    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Countries.AnyAsync(c => c.Id == id, token);
    }

    public Task<List<CountryDTO>> GetAllCountriesAsync(CancellationToken token = default)
    {
        return dbContext.Countries.AsNoTracking().Select(c => new CountryDTO(c.Id, c.Name, c.IsOfficiallyRecognizedCountry)).ToListAsync(token);
    }

    public async Task<Country> GetCountryByIdAsync(string countryId, CancellationToken token = default)
    {
        Country country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == countryId, token)
            ?? throw new KeyNotFoundException($"No country with id {countryId} could be found.");

        return country;
    }

    public async Task<string> GetIdByNameAsync(string countryName, CancellationToken token = default)
    {
        Country country = await dbContext.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Name == countryName, token)
            ?? throw new KeyNotFoundException($"No country with name {countryName} could be found.");

        return country.Id;
    }
}
