using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class AirportService : IAirportService
    {
        private readonly BuzzAirDbContext _context;

        public AirportService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<Airport> Create(string icao, string iata, string name, City city, State state, Country country, int elevation, double lat, double lgt, string tz)
        {
            Airport airport = new Airport()
            {
                Id = Guid.NewGuid().ToString(),
                ICAO = icao,
                IATA = iata,
                Name = name,
                City = city,
                State = state,
                Country = country,
                Elevation = elevation,
                Latitude = lat,
                Longitude = lgt,
                TimeZone = tz
            };

            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();

            return airport;
        }

        public async Task<List<Airport>> GetAllAsQueryable(int pageSize, int? pageNumber)
        {
            int toSkip = ((pageNumber ?? 1) - 1) * pageSize;

            return await _context.Airports.Include(x => x.City).Include(x => x.State).Include(x => x.Country).Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .AsSplitQuery().AsQueryable()
                .Skip(toSkip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Airports.Where(x => !x.IsDeleted).CountAsync();
        }

        public async Task<bool> Exists(string id)
        {
            return await _context.Airports.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Airports.AnyAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Airport>> GetAll()
        {
            return await _context.Airports.Include(x => x.Country).AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetAllForCountry(string countryId)
        {
            return await _context.Airports.Include(x => x.Country).Where(x => x.CountryId == countryId).AsSplitQuery().ToListAsync();
        }

        public async Task<Airport> GetById(string id)
        {
            return await _context.Airports.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Airport> GetByName(string name)
        {
            return await _context.Airports.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
