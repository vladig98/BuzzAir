namespace BuzzAir.Areas.Admin.Repositories
{
    public class CountryRepository(BuzzAirDbContext dbContext) : ICountryRepository
    {
        public Task<List<Country>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default) =>
            GetAllCountriesAsync(pageNumber, itemsPerPage, isDeleted: false, token);

        public Task<List<Country>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default) =>
            GetAllCountriesAsync(pageNumber, itemsPerPage, isDeleted: true, token);

        public async Task CreateAsync(Country country, CancellationToken token = default)
        {
            await dbContext.Countries.AddAsync(country, token);
            await dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(string id, CancellationToken token = default)
        {
            Country country = await GetCountryByIdAsync(id, isDeleted: false, token);
            country.IsDeleted = true;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task RestoreAsync(string id, CancellationToken token = default)
        {
            Country country = await GetCountryByIdAsync(id, isDeleted: true, token);
            country.IsDeleted = false;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task EditAsync(Country country, CancellationToken token = default)
        {
            if (dbContext.Entry(country).State != EntityState.Modified)
            {
                return;
            }

            await dbContext.SaveChangesAsync(token);
        }

        public Task<Country> GetByIdAsync(string id, CancellationToken token = default) =>
            GetCountryByIdAsync(id, isDeleted: false, token);

        public Task<Country> GetDeletedByIdAsync(string id, CancellationToken token = default) =>
            GetCountryByIdAsync(id, isDeleted: true, token);

        public Task<long> GetCountAsync(CancellationToken token = default) =>
            GetCountriesCountAsync(isDeleted: false, token);

        public Task<long> GetDeletedCountAsync(CancellationToken token = default) =>
            GetCountriesCountAsync(isDeleted: true, token);

        private Task<long> GetCountriesCountAsync(bool isDeleted, CancellationToken token) =>
            dbContext.Countries.LongCountAsync(x => x.IsDeleted == isDeleted, token);

        private async Task<Country> GetCountryByIdAsync(string id, bool isDeleted, CancellationToken token) =>
            await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == isDeleted, token) ??
                throw new KeyNotFoundException($"No country with id {id}.");

        private Task<List<Country>> GetAllCountriesAsync(int? pageNumber, int? itemsPerPage, bool isDeleted, CancellationToken token)
        {
            IQueryable<Country> query = dbContext.Countries
                .Where(x => x.IsDeleted == isDeleted)
                .OrderBy(x => x.Name);

            if (itemsPerPage.HasValue && itemsPerPage.Value > 0)
            {
                int page = pageNumber ?? 0;
                query = query.Skip(page * itemsPerPage.Value).Take(itemsPerPage.Value);
            }

            query = query.AsSplitQuery().AsNoTracking();

            return query.ToListAsync(token);
        }
    }
}
