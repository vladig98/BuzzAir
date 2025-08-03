
namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ITimezoneService
{
    Task<Timezone> GetTimezoneByIdAsync(string timezoneId, CancellationToken token);
    Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default);
}
