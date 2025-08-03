namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ITimezoneService
{
    Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default);
}
