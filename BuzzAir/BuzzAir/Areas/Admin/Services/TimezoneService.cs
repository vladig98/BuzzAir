namespace BuzzAir.Areas.Admin.Services;

public class TimezoneService(BuzzAirDbContext dbContext) : ITimezoneService
{
    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.Timezones.AnyAsync(t => t.Id == id, token);
    }

    public async Task<Timezone> GetTimezoneByIdAsync(string timezoneId, CancellationToken token = default)
    {
        Timezone timezone = await dbContext.Timezones.FirstOrDefaultAsync(t => t.Id == timezoneId, token)
            ?? throw new KeyNotFoundException($"Can't find a timezone with id {timezoneId}.");

        return timezone;
    }

    public Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default)
    {
        return dbContext.Timezones.AsNoTracking().Select(t => new TimezoneDTO(t.Id, t.Name)).ToListAsync(token);
    }
}
