namespace BuzzAir.Areas.Admin.Services;

public class AirportService(
    BuzzAirDbContext dbContext,
    ICityService cityService) : IAirportService
{
    public async Task AddAirportAsync(CreateAirportVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        City city = await cityService.GetCityModelByIdAsync(viewModel.CityId, token);

        Airport airport = new()
        {
            Name = viewModel.Name,
            IATA = viewModel.IATA,
            ICAO = viewModel.ICAO,
            Latitude = viewModel.Latitude,
            Longitude = viewModel.Longitude,
            ElevationAboveSeaLevel = viewModel.ElevationAboveSeaLevel,
            City = city
        };

        _ = await dbContext.Airports.AddAsync(airport, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    public Task<int> CountAsync(CancellationToken token = default)
    {
        return dbContext.Airports.CountAsync(a => !a.IsDeleted, token);
    }

    public Task<int> CountDeletedAsync(CancellationToken token = default)
    {
        return dbContext.Airports.CountAsync(a => a.IsDeleted, token);
    }

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Airports.Where(c => c.Id == id && !c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Airports.AnyAsync(a => a.Id == id && !a.IsDeleted, token);
    }

    public Task<bool> ExistsByIATAAsync(string iata, string? id = null, CancellationToken token = default)
    {
        return id is null
            ? dbContext.Airports.AnyAsync(a => a.IATA == iata && !a.IsDeleted, token)
            : dbContext.Airports.AnyAsync(a => a.IATA == iata && !a.IsDeleted && a.Id != id, token);
    }

    public Task<bool> ExistsByICAOAsync(string icao, string? id = null, CancellationToken token = default)
    {
        return id is null
            ? dbContext.Airports.AnyAsync(a => a.ICAO == icao && !a.IsDeleted, token)
            : dbContext.Airports.AnyAsync(a => a.ICAO == icao && !a.IsDeleted && a.Id != id, token);
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken token = default)
    {
        return dbContext.Airports.AnyAsync(a => a.Name == name && !a.IsDeleted, token);
    }

    public async Task<AirportDTO> GetAirportByIdAsync(string id, CancellationToken token = default)
    {
        Airport airport = await dbContext.Airports
                            .Include(a => a.City)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find an airport with id {id}");

        AirportDTO dto = new(
            airport.Id,
            airport.Name,
            airport.ICAO,
            airport.IATA,
            airport.City.Name,
            airport.CityId,
            airport.Latitude,
            airport.Longitude,
            airport.ElevationAboveSeaLevel
        );

        return dto;
    }

    public async Task<Airport> GetAirportModelByIdAsync(string id, CancellationToken token = default)
    {
        Airport airport = await dbContext.Airports.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find an airport with id {id}");

        return airport;
    }

    public async Task<List<AirportDTO>> GetAllAirportsAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Airports.CountAsync(c => !c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<Airport> airportsQuery = dbContext.Airports
            .Include(c => c.City)
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            airportsQuery = airportsQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<Airport> airports = await airportsQuery.AsNoTracking().ToListAsync(token);
        List<AirportDTO> dtos = [.. airports.Select(c => new AirportDTO(
            c.Id,
            c.Name,
            c.ICAO,
            c.IATA,
            c.City.Name,
            c.CityId,
            c.Latitude,
            c.Longitude,
            c.ElevationAboveSeaLevel)
        )];

        return dtos;
    }

    public async Task<List<AirportDTO>> GetAllDeletedAirportsAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Airports.CountAsync(c => c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<Airport> airportsQuery = dbContext.Airports
            .Include(c => c.City)
            .Where(c => c.IsDeleted)
            .OrderBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            airportsQuery = airportsQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<Airport> airports = await airportsQuery.AsNoTracking().ToListAsync(token);
        List<AirportDTO> dtos = [.. airports.Select(c => new AirportDTO(
            c.Id,
            c.Name,
            c.ICAO,
            c.IATA,
            c.City.Name,
            c.CityId,
            c.Latitude,
            c.Longitude,
            c.ElevationAboveSeaLevel)
        )];

        return dtos;
    }

    public async Task<AirportDTO> GetDeletedAirportByIdAsync(string id, CancellationToken token = default)
    {
        Airport airport = await dbContext.Airports
                                    .Include(a => a.City)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find an airport with id {id}");

        AirportDTO dto = new(
            airport.Id,
            airport.Name,
            airport.ICAO,
            airport.IATA,
            airport.City.Name,
            airport.CityId,
            airport.Latitude,
            airport.Longitude,
            airport.ElevationAboveSeaLevel
        );

        return dto;
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Airports.Where(c => c.Id == id && !c.IsDeleted).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Airports.Where(c => c.Id == id && c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateAirportAsync(EditAirportVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Airports.Where(c => c.Id == viewModel.Id && !c.IsDeleted)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    c => c.Name,
                    c => viewModel.Name)
                .SetProperty(
                    c => c.ICAO,
                    c => viewModel.ICAO)
                .SetProperty(
                    c => c.IATA,
                    c => viewModel.IATA)
                .SetProperty(
                    c => c.Latitude,
                    c => viewModel.Latitude)
                .SetProperty(
                    c => c.Longitude,
                    c => viewModel.Longitude)
                .SetProperty(
                    c => c.ElevationAboveSeaLevel,
                    c => viewModel.ElevationAboveSeaLevel)
                .SetProperty(
                    c => c.CityId,
                    c => viewModel.CityId), token);
    }
}
