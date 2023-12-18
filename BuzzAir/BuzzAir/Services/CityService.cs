using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class CityService : ICityService
    {
        private readonly BuzzAirDbContext _context;

        public CityService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<City> Create(Country country, string name, State? state = null)
        {
            var city = new City()
            {
                Country = country,
                Id = Guid.NewGuid().ToString(),
                Name = name
            };

            if (state != null)
            {
                city.State = state;
            }

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();

            return city;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _context.Cities.Include(x => x.State).AsSplitQuery().ToListAsync();
        }

        public async Task<City> GetById(string id)
        {
            return await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<City> GetByName(string name)
        {
            return await _context.Cities.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
