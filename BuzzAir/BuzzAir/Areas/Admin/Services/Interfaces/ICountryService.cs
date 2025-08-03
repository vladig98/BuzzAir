namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ICountryService
{
    Task<List<CountryDTO>> GetAllCountriesAsync(CancellationToken token = default);
}
