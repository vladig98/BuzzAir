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

        public async Task<Aircraft> Delete(string id)
        {
            var aircraft = await _context.Aircrafts.FirstOrDefaultAsync(x => x.Id == id);

            aircraft.IsDeleted = true;

            await _context.SaveChangesAsync();

            return aircraft;
        }

        public async Task<bool> CanChangeSeats(string id, int numberOfSeats)
        {
            var fligths = await _context.Flights.Include(x => x.Aircraft).Where(x => x.AircraftId == id).ToListAsync();

            if (fligths.Any(x => x.Passengers.Count > numberOfSeats))
            {
                return false;
            }

            return true;
        }

        public async Task<Aircraft> Edit(string id, string name, int numberOfSeats)
        {
            var aircraft = await _context.Aircrafts.FirstOrDefaultAsync(x => x.Id == id);

            aircraft.Name = name;
            aircraft.NumberOfSeats = numberOfSeats;

            await _context.SaveChangesAsync();

            return aircraft;
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

        public async Task<List<Aircraft>> GetAllAsQueryable(int pageSize, int? pageNumber)
        {
            int toSkip = ((pageNumber ?? 1) - 1) * pageSize;

            return await _context.Aircrafts.Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .AsSplitQuery().AsQueryable()
                .Skip(toSkip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Aircrafts.Where(x => !x.IsDeleted).CountAsync();
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
