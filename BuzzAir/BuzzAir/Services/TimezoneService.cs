namespace BuzzAir.Services
{
    public class TimezoneService(BuzzAirDbContext context) : ITimezoneService
    {
        public async Task<IEnumerable<Timezone>> GetAllAsync()
        {
            return await context.Timezones.AsNoTracking().ToListAsync();
        }
    }
}
