using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class TimezoneService : ITimezoneService
    {
        private readonly BuzzAirDbContext _context;

        public TimezoneService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Timezone>> GetAll()
        {
            return await _context.Timezones.ToListAsync();
        }
    }
}
