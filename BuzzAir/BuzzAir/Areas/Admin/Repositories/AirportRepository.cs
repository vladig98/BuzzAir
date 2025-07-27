namespace BuzzAir.Areas.Admin.Repositories
{
    public class AirportRepository(BuzzAirDbContext dbContext) : IAirportRepository
    {
        public Task<List<Airport>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, AirportEnum includes = AirportEnum.None, CancellationToken token = default) =>
            GetAllAirportsAsync(pageNumber, itemsPerPage, isDeleted: false, includes, token);

        public Task<List<Airport>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, AirportEnum includes = AirportEnum.None, CancellationToken token = default) =>
            GetAllAirportsAsync(pageNumber, itemsPerPage, isDeleted: true, includes, token);

        public Task<bool> CanChangeLocationAsync(string id, CancellationToken token = default) =>
            dbContext.Airports
                .Where(x => x.Id == id)
                .Include(x => x.FlightsFrom.Where(flight => flight.ArrivalUTC > DateTime.UtcNow))
                .Include(x => x.FlightsTo.Where(flight => flight.ArrivalUTC > DateTime.UtcNow))
                .AnyAsync(x => x.FlightsFrom.Count > 0 && x.FlightsTo.Count > 0, token);

        public async Task CreateAsync(Airport airport, CancellationToken token = default)
        {
            await dbContext.Airports.AddAsync(airport, token);
            await dbContext.SaveChangesAsync(token);
        }

        public async Task EditAsync(Airport airport, CancellationToken token = default)
        {
            if (dbContext.Entry(airport).State != EntityState.Modified)
            {
                return;
            }

            await dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default)
        {
            Airport airport = await GetAirportByIdAsync(id, isDeleted: false, includes, token);
            airport.IsDeleted = true;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task RestoreAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default)
        {
            Airport airport = await GetAirportByIdAsync(id, isDeleted: true, includes, token);
            airport.IsDeleted = false;

            await dbContext.SaveChangesAsync(token);
        }

        public Task<Airport> GetByIdAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default) =>
            GetAirportByIdAsync(id, isDeleted: false, includes, token);

        public Task<Airport> GetDeletedByIdAsync(string id, AirportEnum includes = AirportEnum.None, CancellationToken token = default) =>
            GetAirportByIdAsync(id, isDeleted: true, includes, token);

        public Task<long> GetCountAsync(CancellationToken token = default) =>
            GetAiportsCountAsync(isDeleted: false, token);

        public Task<long> GetDeletedCountAsync(CancellationToken token = default) =>
            GetAiportsCountAsync(isDeleted: true, token);

        private Task<long> GetAiportsCountAsync(bool isDeleted, CancellationToken token) =>
            dbContext.Airports.LongCountAsync(x => x.IsDeleted == isDeleted, token);

        private async Task<Airport> GetAirportByIdAsync(string id, bool isDeleted, AirportEnum includes, CancellationToken token)
        {
            IQueryable<Airport> query = dbContext.Airports;
            query = AttachIncludes(query, includes);

            Airport airport = await query
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == isDeleted, token) ??
                throw new KeyNotFoundException($"Can't find an airport with id {id}");

            return airport;
        }

        private Task<List<Airport>> GetAllAirportsAsync(int? pageNumber, int? itemsPerPage, bool isDeleted, AirportEnum includes, CancellationToken token)
        {
            IQueryable<Airport> query = dbContext.Airports
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

        private static IQueryable<Airport> AttachIncludes(IQueryable<Airport> query, AirportEnum includes)
        {
            if (includes == AirportEnum.None)
            {
                return query;
            }

            if (includes == AirportEnum.All)
            {
                return query
                    .Include(x => x.City)
                    .Include(x => x.Country)
                    .Include(x => x.State);
            }

            if (includes.HasFlag(AirportEnum.City))
            {
                query = query.Include(x => x.City);
            }

            if (includes.HasFlag(AirportEnum.Country))
            {
                query = query.Include(x => x.Country);
            }

            if (includes.HasFlag(AirportEnum.State))
            {
                query = query.Include(x => x.State);
            }

            return query;
        }
    }
}
