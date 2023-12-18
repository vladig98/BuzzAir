using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class BookingFlightService : IBookingFlightService
    {
        private readonly BuzzAirDbContext _context;

        public BookingFlightService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<BookingFlight> Create(Flight flight, Booking booking, bool isOutbound = false)
        {
            BookingFlight bookingFlight = new BookingFlight()
            {
                Flight = flight,
                FlightId = flight.Id,
                Booking = booking,
                BookingId = booking.Id,
                Id = Guid.NewGuid().ToString(),
                IsOutbound = isOutbound
            };

            await _context.BookingFlights.AddAsync(bookingFlight);
            await _context.SaveChangesAsync();

            return bookingFlight;
        }
    }
}
