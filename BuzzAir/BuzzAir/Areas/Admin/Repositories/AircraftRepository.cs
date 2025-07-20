namespace BuzzAir.Areas.Admin.Repositories
{
    public class AircraftRepository(BuzzAirDbContext dbContext) : IAircraftRepository
    {
        public async Task CreateAsync(Aircraft aircraft, CancellationToken token = default)
        {
            await dbContext.Aircrafts.AddAsync(aircraft, token);
            await dbContext.SaveChangesAsync(token);
        }

        public async Task EditAsync(Aircraft aircraft, CancellationToken token = default)
        {
            if (dbContext.Entry(aircraft).State != EntityState.Modified)
            {
                return;
            }

            await dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(string id, CancellationToken token = default)
        {
            Aircraft aircraft = await GetAircraftByIdAsync(id, isDeleted: false, token);
            aircraft.IsDeleted = true;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task RestoreAsync(string id, CancellationToken token = default)
        {
            Aircraft aircraft = await GetAircraftByIdAsync(id, isDeleted: true, token);
            aircraft.IsDeleted = false;

            await dbContext.SaveChangesAsync(token);
        }

        public Task<bool> CanChangeSeatsAsync(string id, int newSeatsNumber, CancellationToken token = default) =>
            dbContext.Flights
                .Where(flight =>
                    flight.Arrival > DateTime.UtcNow &&
                    flight.AircraftId == id &&
                    !flight.IsDeleted)
                .AllAsync(x => x.Seats.Count <= newSeatsNumber, token);

        public Task<Aircraft> GetByIdAsync(string id, CancellationToken token = default) =>
            GetAircraftByIdAsync(id, isDeleted: false, token);

        public Task<Aircraft> GetDeletedByIdAsync(string id, CancellationToken token = default) =>
            GetAircraftByIdAsync(id, isDeleted: true, token);

        public Task<List<Aircraft>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default) =>
            GetAllAircraftAsync(pageNumber, itemsPerPage, isDeleted: false, token);

        public Task<List<Aircraft>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, CancellationToken token = default) =>
            GetAllAircraftAsync(pageNumber, itemsPerPage, isDeleted: true, token);

        public Task<long> GetCountAsync(CancellationToken token = default) =>
            GetAircraftCountAsync(isDeleted: false, token);

        public Task<long> GetDeletedCountAsync(CancellationToken token = default) =>
            GetAircraftCountAsync(isDeleted: true, token);

        private Task<long> GetAircraftCountAsync(bool isDeleted, CancellationToken token) =>
            dbContext.Aircrafts.LongCountAsync(x => x.IsDeleted == isDeleted, token);

        private Task<List<Aircraft>> GetAllAircraftAsync(int? pageNumber, int? itemsPerPage, bool isDeleted, CancellationToken token)
        {
            IQueryable<Aircraft> query = dbContext.Aircrafts
                .AsNoTracking()
                .Where(x => x.IsDeleted == isDeleted)
                .OrderBy(x => x.Name);

            if (itemsPerPage.HasValue && itemsPerPage.Value > 0)
            {
                int page = pageNumber ?? 0;
                query = query.Skip(page * itemsPerPage.Value).Take(itemsPerPage.Value);
            }

            return query.ToListAsync(token);
        }

        private async Task<Aircraft> GetAircraftByIdAsync(string id, bool isDeleted, CancellationToken token) =>
            await dbContext.Aircrafts.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == isDeleted, token) ??
                throw new KeyNotFoundException($"There is no aircraft with id {id} in the database");
    }
}
