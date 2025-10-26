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

        Country country = await countryService.GetCountryModelByIdAsync(model.CountryId, token);
        State? state = await stateService.GetStateModelByIdAsync(model.StateId, token);
        Timezone timezone = await timezoneService.GetTimezoneModelByIdAsync(model.TimezoneId, token);

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

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Cities.Where(c => c.Id == id && !c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public Task<bool> ExistsByIdAsync(string id, CancellationToken token = default)
    {
        return dbContext.Cities.AnyAsync(c => c.Id == id && !c.IsDeleted, token);
    }

    public async Task<List<CityDTO>> GetAllCitiiesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Cities.CountAsync(c => !c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<City> citiesQuery = dbContext.Cities
            .Include(c => c.Country)
            .Include(c => c.State)
            .Include(c => c.Timezone)
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            citiesQuery = citiesQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<City> cities = await citiesQuery.AsNoTracking().ToListAsync(token);
        List<CityDTO> dtos = [.. cities.Select(c => new CityDTO(c.Id, c.Name, c.Country.Name, c.State?.Name, c.Timezone.Name))];

        return dtos;
    }

    public Task<Dictionary<string, Dictionary<string, string>>> GetAllCitiiesPaginatedAsync(int pageIndex, int itemsPerPage, string currentSearch, CancellationToken token)
    {
        IQueryable<City> query = dbContext.Cities.AsNoTracking()
                                                 .Include(x => x.Country);

        return !string.IsNullOrWhiteSpace(currentSearch)
            ? query.Where(x => EF.Functions.ILike(x.Name, $"%{currentSearch}%"))
                   .Take(itemsPerPage)
                   .ToDictionaryAsync(x => x.Country.Name, x => new Dictionary<string, string>() { { x.Id, x.Name } }, token)
            : query.Skip(itemsPerPage * pageIndex)
                   .Take(itemsPerPage)
                   .ToDictionaryAsync(x => x.Country.Name, x => new Dictionary<string, string>() { { x.Id, x.Name } }, token);
    }

    public async Task<List<CityDTO>> GetAllDeletedCitiiesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Cities.CountAsync(c => c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<City> citiesQuery = dbContext.Cities
            .Include(c => c.Country)
            .Include(c => c.State)
            .Include(c => c.Timezone)
            .Where(c => c.IsDeleted)
            .OrderBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            citiesQuery = citiesQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<City> cities = await citiesQuery.AsNoTracking().ToListAsync(token);
        List<CityDTO> dtos = [.. cities.Select(c => new CityDTO(c.Id, c.Name, c.Country.Name, c.State?.Name, c.Timezone.Name))];

        return dtos;
    }

    public Task<List<CityDTO>> GetCitiesByStateAndCountryAsync(string? stateId, string countryId, CancellationToken token = default)
    {
        IQueryable<City> cities = dbContext.Cities.Include(c => c.State)
                                                  .Include(c => c.Country)
                                                  .Include(c => c.Timezone)
                                                  .Where(c => !c.IsDeleted);

        if (!string.IsNullOrWhiteSpace(stateId))
        {
            cities = cities.Where(c => c.StateId == stateId && c.State!.CountryId == countryId);
        }

        cities = cities.Where(c => c.CountryId == countryId);

        return cities
            .Select(c =>
                new CityDTO(
                    c.Id,
                    c.Name,
                    c.Country.Name,
                    c.State == null ? null : c.State.Name,
                    c.Timezone.Name))
            .ToListAsync(token);
    }

    public async Task<CityDTO> GetCityByIdAsync(string id, CancellationToken token = default)
    {
        City city = await dbContext.Cities.Include(c => c.Country)
                                          .Include(c => c.State)
                                          .Include(c => c.Timezone)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find a city with id {id}");

        CityDTO dto = new(city.Id, city.Name, city.Country.Name, city.State?.Name, city.Timezone.Name);

        return dto;
    }

    public async Task<City> GetCityModelByIdAsync(string cityId, CancellationToken token = default)
    {
        return await dbContext.Cities.FirstOrDefaultAsync(c => c.Id == cityId && !c.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find a city with id {cityId}");
    }

    public Task<int> GetCountAsync(CancellationToken token = default)
    {
        return dbContext.Cities.CountAsync(c => !c.IsDeleted, token);
    }

    public async Task<CityDTO> GetDeletedCityByIdAsync(string id, CancellationToken token = default)
    {
        City city = await dbContext.Cities.Include(c => c.Country)
                                          .Include(c => c.State)
                                          .Include(c => c.Timezone)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find a city with id {id}");

        CityDTO dto = new(city.Id, city.Name, city.Country.Name, city.State?.Name, city.Timezone.Name);

        return dto;
    }

    public Task<int> GetDeletedCountAsync(CancellationToken token = default)
    {
        return dbContext.Cities.CountAsync(c => c.IsDeleted, token);
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Cities.Where(c => c.Id == id && !c.IsDeleted).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Cities.Where(c => c.Id == id && c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateCityAsync(EditCityVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Cities.Where(c => c.Id == viewModel.Id && !c.IsDeleted)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    c => c.StateId,
                    c => viewModel.StateId)
                .SetProperty(
                    c => c.CountryId,
                    c => viewModel.CountryId)
                .SetProperty(
                    c => c.TimezoneId,
                    c => viewModel.TimezoneId)
                .SetProperty(
                    c => c.Name,
                    c => viewModel.Name), token);
    }
}
