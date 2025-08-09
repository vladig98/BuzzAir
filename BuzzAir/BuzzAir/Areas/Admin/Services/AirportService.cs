namespace BuzzAir.Areas.Admin.Services;

public class AirportService(BuzzAirDbContext dbContext) : IAirportService
{
    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Airports.AnyAsync(a => a.Id == id && !a.IsDeleted, token);
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken token = default)
    {
        return dbContext.Airports.AnyAsync(a => a.Name == name && !a.IsDeleted, token);
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
}
