using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class FlightPassengerService : IFlightPassengerService
    {
        private readonly BuzzAirDbContext _context;
        private readonly IFlightsService _flightService;

        public FlightPassengerService(BuzzAirDbContext context, IFlightsService flightService)
        {
            _context = context;
            _flightService = flightService;

        }

        public async Task<FlightPassenger> Create(IPassenger passenger, Flight flight)
        {
            Random rnd = new Random();

            List<FlightSeat> availableSeats = _context.FlightSeats.Where(x => x.FlightId == flight.Id).Where(x => x.IsAvailable).ToList();

            int seatNumber = rnd.Next(0, availableSeats.Count);
            FlightSeat seat = flight.Seats.FirstOrDefault(x => x.SeatNumber == seatNumber);

            seat.IsAvailable = false;

            var flightPax = new FlightPassenger
            {
                Flight = flight,
                FlightId = flight.Id,
                Id = Guid.NewGuid().ToString(),
                Person = (Person)passenger,
                PersonId = ((Person)passenger).Id,
                SeatNumber = seatNumber
            };

            await _context.FlightPassengers.AddAsync(flightPax);
            await _context.SaveChangesAsync();

            return flightPax;
        }
    }
}
