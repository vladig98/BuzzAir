namespace BuzzAir.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(string id);
        Task<List<SelectListItem>> GetCountriesForSelect();
    }
}
