
namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ICountryService
{
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
    Task<List<CountryDTO>> GetAllCountriesAsync(CancellationToken token = default);
    Task<Country> GetCountryByIdAsync(string countryId, CancellationToken token = default);
    Task<string> GetIdByNameAsync(string countryName, CancellationToken token = default);
}
