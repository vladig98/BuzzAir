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

        Country country = await countryService.GetCountryByIdAsync(model.CountryId, token);
        State? state = await stateService.GetStateModelByIdAsync(model.StateId, token);
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

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Cities.Where(c => c.Id == id).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public async Task<List<CityDTO>> GetAllCitiiesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Cities.CountAsync(c => !c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<City> cities = await dbContext.Cities
            .Include(c => c.Country)
            .Include(c => c.State)
            .Include(c => c.Timezone)
            .Where(x => !x.IsDeleted)
            .OrderBy(c => c.Name)
            .Skip((pageNumber - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .AsNoTracking()
            .ToListAsync(token);

        List<CityDTO> dtos = [.. cities.Select(c => new CityDTO(c.Id, c.Name, c.Country.Name, c.State?.Name, c.Timezone.Name))];

        return dtos;
    }

    public async Task<List<CityDTO>> GetAllDeletedCitiiesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Cities.CountAsync(c => c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<City> cities = await dbContext.Cities
            .Include(c => c.Country)
            .Include(c => c.State)
            .Include(c => c.Timezone)
            .Where(x => x.IsDeleted)
            .OrderBy(c => c.Name)
            .Skip((pageNumber - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .AsNoTracking()
            .ToListAsync(token);

        List<CityDTO> dtos = [.. cities.Select(c => new CityDTO(c.Id, c.Name, c.Country.Name, c.State?.Name, c.Timezone.Name))];

        return dtos;
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
        return dbContext.Cities.Where(c => c.Id == id).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Cities.Where(c => c.Id == id).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateCityAsync(EditCityVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Cities.Where(c => c.Id == viewModel.Id)
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
