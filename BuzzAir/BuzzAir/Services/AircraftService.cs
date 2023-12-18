using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly BuzzAirDbContext _context;

        public AircraftService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<Aircraft> Create(string name, int numberOFSeats)
        {
            Aircraft aircraft = new Aircraft()
            {
                Name = name,
                NumberOfSeats = numberOFSeats,
                Id = Guid.NewGuid().ToString()
            };

            await _context.Aircrafts.AddAsync(aircraft);
            await _context.SaveChangesAsync();

            return aircraft;
        }

        public async Task<bool> ExistById(string id)
        {
            return await _context.Aircrafts.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Aircrafts.AnyAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Aircraft>> GetAll()
        {
            return await _context.Aircrafts.ToListAsync();
        }

        public async Task<Aircraft> GetById(string id)
        {
            return await _context.Aircrafts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Aircraft> GetByName(string name)
        {
            return await _context.Aircrafts.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
