namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ICountryService
{
    Task AddCountryAsync(CreateCountryVM viewModel, CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<List<CountryDTO>> GetAllCountriesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<List<CountryDTO>> GetAllDeletedCountriesAsync(int? pageNumber, int? itemsPerPage, CancellationToken token = default);
    Task<int> GetCountAsync(CancellationToken token = default);
    Task<Country> GetCountryModelByIdAsync(string countryId, CancellationToken token = default);
    Task<CountryDTO> GetCountryByIdAsync(string countryId, CancellationToken token = default);
    Task<int> GetDeletedCountAsync(CancellationToken token = default);
    Task<string> GetIdByNameAsync(string countryName, CancellationToken token = default);
    Task UpdateCountryAsync(EditCountryVM viewModel, CancellationToken token = default);
    Task<CountryDTO> GetDeletedCountryByIdAsync(string id, CancellationToken token = default);
    Task RestoreAsync(string id, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
    Task HardDeleteAsync(string id, CancellationToken token = default);
}
