namespace BuzzAir.Areas.Admin.Repositories
{
    public class CityRepository(BuzzAirDbContext dbContext) : ICityRepository
    {
        public Task<List<City>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, CityEnum includes = CityEnum.None, CancellationToken token = default) =>
            GetAllCitiesAsync(pageNumber, itemsPerPage, isDeleted: false, includes, token);

        public Task<List<City>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, CityEnum includes = CityEnum.None, CancellationToken token = default) =>
            GetAllCitiesAsync(pageNumber, itemsPerPage, isDeleted: true, includes, token);

        public async Task CreateAsync(City city, CancellationToken token = default)
        {
            await dbContext.Cities.AddAsync(city, token);
            await dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(string id, CityEnum includes, CancellationToken token = default)
        {
            City city = await GetCityByIdAsync(id, isDeleted: false, includes, token);
            city.IsDeleted = true;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task RestoreAsync(string id, CityEnum includes, CancellationToken token = default)
        {
            City city = await GetCityByIdAsync(id, isDeleted: true, includes, token);
            city.IsDeleted = false;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task EditAsync(City city, CancellationToken token = default)
        {
            if (dbContext.Entry(city).State != EntityState.Modified)
            {
                return;
            }

            await dbContext.SaveChangesAsync(token);
        }

        public async Task<bool> CanChangeLocationAsync(string id, CancellationToken token)
        {
            bool hasFlightsFromCity = await dbContext.Flights
                .Include(x => x.Origin)
                .Where(x => x.Arrival > DateTime.UtcNow)
                .AnyAsync(x => x.Origin.CityId == id, token);

            bool hasFlightsToCity = await dbContext.Flights
                .Include(x => x.Destination)
                .Where(x => x.Arrival > DateTime.UtcNow)
                .AnyAsync(x => x.Destination.CityId == id, token);

            return !hasFlightsFromCity && !hasFlightsToCity;
        }

        public Task<City> GetByIdAsync(string id, CityEnum includes, CancellationToken token = default) =>
            GetCityByIdAsync(id, isDeleted: false, includes, token);

        public Task<long> GetCountAsync(CancellationToken token = default) =>
            GetCitiesCountAsync(isDeleted: false, token);

        public Task<City> GetDeletedByIdAsync(string id, CityEnum includes, CancellationToken token = default) =>
            GetCityByIdAsync(id, isDeleted: true, includes, token);

        public Task<long> GetDeletedCountAsync(CancellationToken token = default) =>
            GetCitiesCountAsync(isDeleted: true, token);

        private Task<long> GetCitiesCountAsync(bool isDeleted, CancellationToken token) =>
            dbContext.Cities.LongCountAsync(x => x.IsDeleted == isDeleted, token);

        private async Task<City> GetCityByIdAsync(string id, bool isDeleted, CityEnum includes, CancellationToken token)
        {
            IQueryable<City> query = dbContext.Cities;
            query = AttachIncludes(query, includes);

            City city = await query
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == isDeleted, token) ??
                throw new KeyNotFoundException($"Can't find a city with id {id}");

            return city;
        }

        private Task<List<City>> GetAllCitiesAsync(int? pageNumber, int? itemsPerPage, bool isDeleted, CityEnum includes, CancellationToken token)
        {
            IQueryable<City> query = dbContext.Cities
                .Where(x => x.IsDeleted == isDeleted)
                .OrderBy(x => x.Name);

            query = AttachIncludes(query, includes);

            if (itemsPerPage.HasValue && itemsPerPage.Value > 0)
            {
                int page = pageNumber ?? 0;
                query = query.Skip(page * itemsPerPage.Value).Take(itemsPerPage.Value);
            }

            query = query.AsSplitQuery().AsNoTracking();

            return query.ToListAsync(token);
        }

        private static IQueryable<City> AttachIncludes(IQueryable<City> query, CityEnum includes)
        {
            if (includes == CityEnum.None)
            {
                return query;
            }

            if (includes == CityEnum.All)
            {
                return query
                    .Include(x => x.Country)
                    .Include(x => x.State);
            }

            if (includes.HasFlag(CityEnum.Country))
            {
                query = query.Include(x => x.Country);
            }

            if (includes.HasFlag(CityEnum.State))
            {
                query = query.Include(x => x.State);
            }

            return query;
        }
    }
}
