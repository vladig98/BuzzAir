namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ICityService
{
    Task AddCityAsync(CreateCityVM model, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task<bool> ExistsByIdAsync(string id, CancellationToken token = default);
    Task<List<CityDTO>> GetAllCitiiesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<List<CityDTO>> GetAllDeletedCitiiesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<List<CityDTO>> GetCitiesByStateAndCountryAsync(string? stateId, string countryId, CancellationToken token = default);
    Task<CityDTO> GetCityByIdAsync(string id, CancellationToken token = default);
    Task<City> GetCityModelByIdAsync(string cityId, CancellationToken token = default);
    Task<int> GetCountAsync(CancellationToken token = default);
    Task<CityDTO> GetDeletedCityByIdAsync(string id, CancellationToken token = default);
    Task<int> GetDeletedCountAsync(CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
    Task UpdateCityAsync(EditCityVM viewModel, CancellationToken token = default);
}
