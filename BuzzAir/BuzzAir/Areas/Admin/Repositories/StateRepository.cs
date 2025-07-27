namespace BuzzAir.Areas.Admin.Repositories
{
    public class StateRepository(BuzzAirDbContext dbContext) : IStateRepository
    {
        public Task<List<State>> AllAsync(int? pageNumber = null, int? itemsPerPage = null, bool includeCountry = false, CancellationToken token = default) =>
            GetAllStatesAsync(pageNumber, itemsPerPage, isDeleted: false, includeCountry, token);

        public Task<List<State>> AllDeletedAsync(int? pageNumber = null, int? itemsPerPage = null, bool includeCountry = false, CancellationToken token = default) =>
            GetAllStatesAsync(pageNumber, itemsPerPage, isDeleted: true, includeCountry, token);

        public async Task<bool> CanChangeLocationAsync(string id, CancellationToken token = default)
        {
            bool hasFlightsFromState = await dbContext.Flights
                .Include(x => x.Origin)
                .Where(x => x.ArrivalUTC > DateTime.UtcNow)
                .AnyAsync(x => x.Origin.StateId == id, token);

            bool hasFlightsToState = await dbContext.Flights
                .Include(x => x.Destination)
                .Where(x => x.ArrivalUTC > DateTime.UtcNow)
                .AnyAsync(x => x.Destination.StateId == id, token);

            return !hasFlightsFromState && !hasFlightsToState;
        }

        public async Task CreateAsync(State state, CancellationToken token)
        {
            await dbContext.States.AddAsync(state, token);
            await dbContext.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(string id, bool includeCountry = false, CancellationToken token = default)
        {
            State state = await GetStateByIdAsync(id, isDeleted: false, includeCountry, token);
            state.IsDeleted = true;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task RestoreAsync(string id, bool includeCountry = false, CancellationToken token = default)
        {
            State state = await GetStateByIdAsync(id, isDeleted: true, includeCountry, token);
            state.IsDeleted = false;

            await dbContext.SaveChangesAsync(token);
        }

        public async Task EditAsync(State state, CancellationToken token = default)
        {
            if (dbContext.Entry(state).State != EntityState.Modified)
            {
                return;
            }

            await dbContext.SaveChangesAsync(token);
        }

        public Task<State> GetByIdAsync(string id, bool includeCountry = false, CancellationToken token = default) =>
            GetStateByIdAsync(id, isDeleted: false, includeCountry, token);

        public Task<State> GetDeletedByIdAsync(string id, bool includeCountry = false, CancellationToken token = default) =>
            GetStateByIdAsync(id, isDeleted: true, includeCountry, token);

        public Task<long> GetCountAsync(CancellationToken token = default) =>
            GetStatesCountAsync(isDeleted: false, token);

        public Task<long> GetDeletedCountAsync(CancellationToken token = default) =>
            GetStatesCountAsync(isDeleted: true, token);

        private Task<long> GetStatesCountAsync(bool isDeleted, CancellationToken token) =>
            dbContext.States.LongCountAsync(x => x.IsDeleted == isDeleted, token);

        private async Task<State> GetStateByIdAsync(string id, bool isDeleted, bool includeCountry, CancellationToken token)
        {
            IQueryable<State> query = dbContext.States;

            if (includeCountry)
            {
                query = query.Include(x => x.Country);
            }

            State state = await query.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == isDeleted, token) ??
                throw new KeyNotFoundException($"No state with id {id}.");

            return state;
        }

        private Task<List<State>> GetAllStatesAsync(int? pageNumber, int? itemsPerPage, bool isDeleted, bool includeCountry, CancellationToken token)
        {
            IQueryable<State> query = dbContext.States
                .Where(x => x.IsDeleted == isDeleted)
                .OrderBy(x => x.Name);

            if (includeCountry)
            {
                query = query.Include(x => x.Country);
            }

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
