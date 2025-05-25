namespace BuzzAir.Services
{
    public class StateService(BuzzAirDbContext context) : IStateService
    {
        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await context.States.Include(x => x.Country).AsSplitQuery().AsNoTracking().ToListAsync();
        }

        public async Task<State?> GetByIdAsync(string id)
        {
            return await context.States.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
