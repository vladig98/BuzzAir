namespace BuzzAir.Areas.Admin.Services;

public class TimezoneService(BuzzAirDbContext dbContext) : ITimezoneService
{
    public async Task<Timezone> GetTimezoneByIdAsync(string timezoneId, CancellationToken token)
    {
        Timezone timezone = await dbContext.Timezones.FirstOrDefaultAsync(t => t.Id == timezoneId, token)
            ?? throw new KeyNotFoundException($"Can't find a timezone with id {timezoneId}.");

        return timezone;
    }

    public Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default)
    {
        return dbContext.Timezones.Select(t => new TimezoneDTO(t.Id, t.Name)).ToListAsync(token);
    }
}
