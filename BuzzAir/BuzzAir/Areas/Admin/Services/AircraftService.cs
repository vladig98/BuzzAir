namespace BuzzAir.Areas.Admin.Services;

public class AircraftService(BuzzAirDbContext dbContext) : IAircraftService
{
    public async Task AddAircraftAsync(CreateAircraftVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        Aircraft aircraft = new()
        {
            Name = viewModel.Name,
            NumberOfSeats = viewModel.NumberOfSeats
        };

        _ = await dbContext.Aircrafts.AddAsync(aircraft, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    public Task<int> CountAsync(CancellationToken token = default)
    {
        return dbContext.Aircrafts.CountAsync(a => !a.IsDeleted, token);
    }

    public Task<int> CountDeletedAsync(CancellationToken token = default)
    {
        return dbContext.Aircrafts.CountAsync(a => a.IsDeleted, token);
    }

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Aircrafts.Where(c => c.Id == id && !c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => true), token);
    }

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

    public async Task<AircraftDTO> GetAircraftByIdAsync(string id, CancellationToken token = default)
    {
        Aircraft aircraft = await dbContext.Aircrafts
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find an aircraft with id {id}");

        AircraftDTO dto = new(aircraft.Id, aircraft.Name, aircraft.NumberOfSeats);

        return dto;
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

    public async Task<List<AircraftDTO>> GetAllDeletedAircraftAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Aircrafts.CountAsync(c => c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        IQueryable<Aircraft> aircraftQuery = dbContext.Aircrafts
            .Where(c => c.IsDeleted)
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

    public async Task<AircraftDTO> GetDeletedAircraftByIdAsync(string id, CancellationToken token = default)
    {
        Aircraft aircraft = await dbContext.Aircrafts
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find an aircraft with id {id}");

        AircraftDTO dto = new(aircraft.Id, aircraft.Name, aircraft.NumberOfSeats);

        return dto;
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Aircrafts.Where(c => c.Id == id && !c.IsDeleted).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Aircrafts.Where(c => c.Id == id && c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateAircraftAsync(EditAircraftVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Aircrafts.Where(c => c.Id == viewModel.Id && !c.IsDeleted)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    c => c.Name,
                    c => viewModel.Name)
                .SetProperty(
                    c => c.NumberOfSeats,
                    c => viewModel.NumberOfSeats), token);
    }
}
