using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class CountryService : ICountryService
    {
        private readonly BuzzAirDbContext _context;

        public CountryService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetById(string id)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
