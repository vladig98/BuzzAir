namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ITimezoneService
{
    Task AddTimezoneAsync(CreateTimezoneVM viewModel, CancellationToken token = default);
    Task<int> CountAsync(CancellationToken token = default);
    Task<int> CountDeletedAsync(CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<List<TimezoneDTO>> GetAllDeletedTimezonesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default);
    Task<List<TimezoneDTO>> GetAllTimezonesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default);
    Task<Timezone> GetTimezoneModelByIdAsync(string timezoneId, CancellationToken token = default);
    Task<TimezoneDTO> GetTimezoneByIdAsync(string timezoneId, CancellationToken token = default);
    Task<List<TimezoneDTO>> GetTimezonesAsync(CancellationToken token = default);
    Task UpdateTimezoneAsync(EditTimezoneVM viewModel, CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task<TimezoneDTO> GetDeletedTimezoneByIdAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
    Task<bool> ExistsByIdentifierAsync(string identifier, CancellationToken token = default);
}
