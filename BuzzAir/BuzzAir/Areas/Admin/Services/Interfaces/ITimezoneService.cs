
namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ITimezoneService
{
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<Timezone> GetTimezoneByIdAsync(string timezoneId, CancellationToken token = default);
    Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default);
}
