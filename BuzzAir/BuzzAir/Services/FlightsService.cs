using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.EditModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly BuzzAirDbContext _context;

        public FlightsService(BuzzAirDbContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(10000);
        }

        public async Task<Flight> Create(Aircraft aircraft, int duration, decimal price, DateTime departure, DateTime arrival, string flightNumber, Airport origin, Airport destination)
        {
            Flight flight = new Flight()
            {
                Aircraft = aircraft,
                Arrival = arrival,
                Departure = departure,
                Destination = destination,
                DurationInMinutes = duration,
                FlightNumber = flightNumber,
                Origin = origin,
                Price = price,
                Id = Guid.NewGuid().ToString(),
                AircraftId = aircraft.Id,
                OriginId = origin.Id,
                DestinationId = destination.Id
            };

            for (int i = 0; i <= aircraft.NumberOfSeats; i++)
            {
                flight.Seats.Add(new FlightSeat()
                {
                    Flight = flight,
                    FlightId = flight.Id,
                    Id = Guid.NewGuid().ToString(),
                    IsAvailable = true,
                    SeatNumber = i
                });
            }

            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();

            return flight;
        }

        public async Task Delete(string flightId)
        {
            Flight flight = await _context.Flights.FirstOrDefaultAsync(x => x.Id == flightId);

            flight.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string id)
        {
            return await _context.Flights.Where(x => !x.IsDeleted).AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsByDestination(string destination)
        {
            return await _context.Flights.Where(x => !x.IsDeleted).Include(x => x.Destination).AsSplitQuery().AnyAsync(x => x.Destination.Name == destination);
        }

        public async Task<bool> ExistsByOrigin(string origin)
        {
            return await _context.Flights.Where(x => !x.IsDeleted).Include(x => x.Origin).AsSplitQuery().AnyAsync(x => x.Origin.Name == origin);
        }

        public async Task<IEnumerable<Flight>> GetAll()
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .Where(x => x.Origin.City.Name == "Sofia")
                .AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByOriginAndDestination(City origin, City destination, DateTime departure)
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.City.Name == origin.Name)
                .Where(x => x.Destination.City.Name == destination.Name)
                .Where(x => x.Departure.Year == departure.Year && x.Departure.Month == departure.Month && x.Departure.Day == departure.Day)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByCityId(string cityId)
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.City.Id == cityId)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsForOriginIdAndDestinationId(string originId, string destinationId)
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.City.Id == originId)
                .Where(x => x.Destination.City.Id == destinationId)
                .Where(x => x.Departure > DateTime.Now)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery().ToListAsync();
        }

        public async Task<Flight> GetByDestination(string destination)
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Destination.Name == destination)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Flight> GetById(string id)
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Id == id)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .Include(x => x.Aircraft)
                .Include(x => x.Seats)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Flight> GetByOrigin(string origin)
        {
            return await _context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.Name == origin)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Flight>> GetAllAsQueryable(int pageSize, int? pageNumber)
        {
            int toSkip = ((pageNumber ?? 1) - 1) * pageSize;

            return await _context.Flights.Where(x => !x.IsDeleted)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Aircraft)
                .Where(x => x.Departure > DateTime.Now)
                .OrderBy(x => x.FlightNumber)
                .AsSplitQuery().AsQueryable()
                .Skip(toSkip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Flights.Where(x => !x.IsDeleted).Where(x => x.Departure > DateTime.Now).CountAsync();
        }

        public async Task Update(FlightEditViewModel model)
        {
            var flight = await GetById(model.Id);

            flight.FlightNumber = model.FlightNumber;
            flight.OriginId = model.Origin;
            flight.DestinationId = model.Destination;
            flight.DurationInMinutes = model.DurationInMinutes;
            flight.Departure = model.Departure;
            flight.Arrival = model.Departure.AddMinutes(model.DurationInMinutes);
            flight.Price = model.Price;
            flight.AircraftId = model.Aircraft;

            await _context.SaveChangesAsync();
        }
    }
}
