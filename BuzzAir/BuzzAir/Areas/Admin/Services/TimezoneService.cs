namespace BuzzAir.Areas.Admin.Services;

public class TimezoneService(BuzzAirDbContext buzzAirDbContext) : ITimezoneService
{
    public Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default)
    {
        return buzzAirDbContext.Timezones.Select(t => new TimezoneDTO(t.Id, t.Name)).ToListAsync(token);
    }
}
