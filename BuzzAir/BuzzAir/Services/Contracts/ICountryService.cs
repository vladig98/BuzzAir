namespace BuzzAir.Services.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(string id);
    }
}
