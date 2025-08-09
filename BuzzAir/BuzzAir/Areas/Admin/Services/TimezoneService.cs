namespace BuzzAir.Areas.Admin.Services;

public class TimezoneService(BuzzAirDbContext dbContext) : ITimezoneService
{
    public async Task AddTimezoneAsync(CreateTimezoneVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        Timezone timezone = new()
        {
            Name = viewModel.Name,
            Abbreviation = viewModel.Abbreviation,
            Identifier = viewModel.Identifier,
            Offset = viewModel.Offset,
            UsesDST = viewModel.UsesDST
        };

        _ = await dbContext.Timezones.AddAsync(timezone, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    public Task<int> CountAsync(CancellationToken token = default)
    {
        return dbContext.Timezones.Where(s => !s.IsDeleted).CountAsync(token);
    }

    public Task<int> CountDeletedAsync(CancellationToken token = default)
    {
        return dbContext.Timezones.Where(s => s.IsDeleted).CountAsync(token);
    }

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Timezones.Where(s => s.Id == id && !s.IsDeleted).ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Timezones.AnyAsync(t => t.Id == id && !t.IsDeleted, token);
    }

    public Task<bool> ExistsByIdentifierAsync(string identifier, CancellationToken token = default)
    {
        return dbContext.Timezones.AnyAsync(t => t.Identifier == identifier && !t.IsDeleted, token);
    }

    public async Task<List<TimezoneDTO>> GetAllDeletedTimezonesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Timezones.CountAsync(s => s.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<Timezone> timezones = await dbContext.Timezones.Where(s => s.IsDeleted)
                                                   .OrderBy(s => s.UsesDST)
                                                   .ThenBy(s => s.Name)
                                                   .Skip((pageNumber - 1) * itemsPerPage)
                                                   .Take(itemsPerPage)
                                                   .AsNoTracking()
                                                   .ToListAsync(token);

        List<TimezoneDTO> dtos = [.. timezones.Select(s => new TimezoneDTO(s.Id, s.Name, s.Offset, s.Identifier, s.Abbreviation, s.UsesDST))];

        return dtos;
    }

    public async Task<List<TimezoneDTO>> GetAllTimezonesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Timezones.CountAsync(s => !s.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<Timezone> timezones = await dbContext.Timezones.Where(s => !s.IsDeleted)
                                                   .OrderBy(s => s.UsesDST)
                                                   .ThenBy(s => s.Name)
                                                   .Skip((pageNumber - 1) * itemsPerPage)
                                                   .Take(itemsPerPage)
                                                   .AsNoTracking()
                                                   .ToListAsync(token);

        List<TimezoneDTO> dtos = [.. timezones.Select(s => new TimezoneDTO(s.Id, s.Name, s.Offset, s.Identifier, s.Abbreviation, s.UsesDST))];

        return dtos;
    }

    public async Task<TimezoneDTO> GetDeletedTimezoneByIdAsync(string id, CancellationToken token = default)
    {
        Timezone timezone = await dbContext.Timezones.FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted, token)
            ?? throw new KeyNotFoundException($"No timezone with id {id}.");

        TimezoneDTO dto = new(timezone.Id, timezone.Name, timezone.Offset, timezone.Identifier, timezone.Abbreviation, timezone.UsesDST);
        return dto;
    }

    public async Task<TimezoneDTO> GetTimezoneByIdAsync(string timezoneId, CancellationToken token = default)
    {
        Timezone timezone = await dbContext.Timezones.FirstOrDefaultAsync(t => t.Id == timezoneId && !t.IsDeleted, token)
            ?? throw new KeyNotFoundException($"No timezone with id {timezoneId}.");

        TimezoneDTO dto = new(timezone.Id, timezone.Name, timezone.Offset, timezone.Identifier, timezone.Abbreviation, timezone.UsesDST);
        return dto;
    }

    public async Task<Timezone> GetTimezoneModelByIdAsync(string timezoneId, CancellationToken token = default)
    {
        Timezone timezone = await dbContext.Timezones.FirstOrDefaultAsync(t => t.Id == timezoneId && !t.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find a timezone with id {timezoneId}.");

        return timezone;
    }

    public Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default)
    {
        return dbContext.Timezones.AsNoTracking().Select(t => new TimezoneDTO(t.Id, t.Name, t.Offset, t.Identifier, t.Abbreviation, t.UsesDST)).ToListAsync(token);
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Timezones.Where(s => s.Id == id && !s.IsDeleted).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Timezones.Where(s => s.Id == id && s.IsDeleted).ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateTimezoneAsync(EditTimezoneVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Timezones.Where(s => s.Id == viewModel.Id && !s.IsDeleted)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    s => s.Name,
                    s => viewModel.Name)
                .SetProperty(
                    s => s.Abbreviation,
                    s => viewModel.Abbreviation)
                .SetProperty(
                    s => s.Identifier,
                    s => viewModel.Identifier)
                .SetProperty(
                    s => s.Offset,
                    s => viewModel.Offset)
                .SetProperty(
                    s => s.UsesDST,
                    s => viewModel.UsesDST), token);
    }
}
