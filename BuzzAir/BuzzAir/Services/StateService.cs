using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class StateService : IStateService
    {
        private readonly BuzzAirDbContext _context;

        public StateService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            return await _context.States.Include(x => x.Country).AsSplitQuery().ToListAsync();
        }

        public async Task<State> GetById(string id)
        {
            return await _context.States.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
