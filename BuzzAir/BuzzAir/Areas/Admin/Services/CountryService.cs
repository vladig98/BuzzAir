namespace BuzzAir.Areas.Admin.Services;

public sealed class CountryService(BuzzAirDbContext dbContext) : ICountryService
{
    public async Task AddCountryAsync(CreateCountryVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        Country country = new()
        {
            Name = viewModel.Name,
            ISOA2 = viewModel.ISO2,
            ISOA3 = viewModel.ISO3,
            IsOfficiallyRecognizedCountry = viewModel.IsOfficiallyRecognizedCountry,
        };

        _ = await dbContext.Countries.AddAsync(country, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Countries.AnyAsync(c => c.Id == id && !c.IsDeleted, token);
    }

    public async Task<List<CountryDTO>> GetAllCountriesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Countries.CountAsync(c => !c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<Country> countriesQuery = dbContext.Countries
            .Where(c => !c.IsDeleted)
            .OrderByDescending(c => c.IsOfficiallyRecognizedCountry)
            .ThenBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            countriesQuery = countriesQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<Country> countries = await countriesQuery.AsNoTracking().ToListAsync(token);
        List<CountryDTO> dtos = [.. countries.Select(c => new CountryDTO(c.Id, c.Name, c.ISOA2, c.ISOA3, c.IsOfficiallyRecognizedCountry))];

        return dtos;
    }

    public async Task<List<CountryDTO>> GetAllDeletedCountriesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Countries.CountAsync(c => c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<Country> countriesQuery = dbContext.Countries
            .Where(c => c.IsDeleted)
            .OrderByDescending(c => c.IsOfficiallyRecognizedCountry)
            .ThenBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            countriesQuery = countriesQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<Country> countries = await countriesQuery.AsNoTracking().ToListAsync(token);
        List<CountryDTO> dtos = [.. countries.Select(c => new CountryDTO(c.Id, c.Name, c.ISOA2, c.ISOA3, c.IsOfficiallyRecognizedCountry))];

        return dtos;
    }

    public Task<int> GetCountAsync(CancellationToken token = default)
    {
        return dbContext.Countries.CountAsync(c => !c.IsDeleted, token);
    }

    public async Task<Country> GetCountryModelByIdAsync(string countryId, CancellationToken token = default)
    {
        Country country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == countryId && !c.IsDeleted, token)
            ?? throw new KeyNotFoundException($"No country with id {countryId} could be found.");

        return country;
    }

    public async Task<CountryDTO> GetCountryByIdAsync(string countryId, CancellationToken token = default)
    {
        Country country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == countryId && !c.IsDeleted, token)
            ?? throw new KeyNotFoundException($"No country with id {countryId} could be found.");

        CountryDTO dto = new(country.Id, country.Name, country.ISOA2, country.ISOA3, country.IsOfficiallyRecognizedCountry);
        return dto;
    }

    public Task<int> GetDeletedCountAsync(CancellationToken token = default)
    {
        return dbContext.Countries.CountAsync(c => c.IsDeleted, token);
    }

    public async Task<string> GetIdByNameAsync(string countryName, CancellationToken token = default)
    {
        Country country = await dbContext.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Name == countryName && !c.IsDeleted, token)
            ?? throw new KeyNotFoundException($"No country with name {countryName} could be found.");

        return country.Id;
    }

    public Task UpdateCountryAsync(EditCountryVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Countries.Where(c => c.Id == viewModel.Id && !c.IsDeleted)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    c => c.ISOA3,
                    c => viewModel.ISO3)
                .SetProperty(
                    c => c.ISOA2,
                    c => viewModel.ISO2)
                .SetProperty(
                    c => c.IsOfficiallyRecognizedCountry,
                    c => viewModel.IsOfficiallyRecognizedCountry)
                .SetProperty(
                    c => c.Name,
                    c => viewModel.Name), token);
    }

    public async Task<CountryDTO> GetDeletedCountryByIdAsync(string id, CancellationToken token = default)
    {
        Country country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted, token)
            ?? throw new KeyNotFoundException($"No country with id {id} could be found.");

        CountryDTO dto = new(country.Id, country.Name, country.ISOA2, country.ISOA3, country.IsOfficiallyRecognizedCountry);
        return dto;
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Countries.Where(c => c.Id == id && c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Countries.Where(c => c.Id == id && !c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Countries.Where(c => c.Id == id && !c.IsDeleted).ExecuteDeleteAsync(token);
    }
}
