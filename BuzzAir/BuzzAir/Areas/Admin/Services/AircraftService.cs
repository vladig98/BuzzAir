namespace BuzzAir.Areas.Admin.Services;

public class AircraftService(BuzzAirDbContext dbContext) : IAircraftService
{
    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Aircrafts.AnyAsync(a => a.Id == id && !a.IsDeleted, token);
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken token = default)
    {
        return dbContext.Aircrafts.AnyAsync(a => a.Name == name && !a.IsDeleted, token);
    }

    public async Task<Aircraft> GetAicraftModelByIdAsync(string id, CancellationToken token = default)
    {
        Aircraft aircraft = await dbContext.Aircrafts.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find an aircraft with id {id}");

        return aircraft;
    }

    public async Task<List<AircraftDTO>> GetAllAircraftAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Aircrafts.CountAsync(c => !c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<Aircraft> aircraftQuery = dbContext.Aircrafts
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Name);

        if (pageNumber is not null && itemsPerPage is not null)
        {
            pageNumber = Math.Clamp(pageNumber.Value, 1, count);
            itemsPerPage = Math.Clamp(itemsPerPage.Value, 10, 100);

            aircraftQuery = aircraftQuery.Skip((pageNumber.Value - 1) * itemsPerPage.Value).Take(itemsPerPage.Value);
        }

        List<Aircraft> aircraft = await aircraftQuery.AsNoTracking().ToListAsync(token);
        List<AircraftDTO> dtos = [.. aircraft.Select(c => new AircraftDTO(c.Id, c.Name, c.NumberOfSeats))];

        return dtos;
    }
}
