namespace BuzzAir.Services
{
    public class CountryService(BuzzAirDbContext context) : ICountryService
    {
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await context.Countries.AsNoTracking().ToListAsync();
        }

        public async Task<Country> GetByIdAsync(string id)
        {
            return await context.Countries.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ArgumentException($"Can't find a country with id {id}.");
        }
    }
}
