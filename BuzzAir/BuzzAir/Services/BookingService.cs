using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class BookingService : IBookingService
    {
        private readonly BuzzAirDbContext _context;
        private readonly IFlightsService _flightsService;

        public BookingService(BuzzAirDbContext context, IFlightsService flightsService)
        {
            _context = context;
            _flightsService = flightsService;
        }

        public async Task<IEnumerable<Booking>> GetAllForUser(IEnumerable<UserBooking> userBookings)
        {
            var bookingsIds = userBookings.Select(y => y.BookingId).ToList();
            var bookings = await _context.Bookings.Where(x => bookingsIds.Contains(x.Id))
                .Include(x => x.Payment)
                .Include(x => x.Flights).ThenInclude(x => x.Flight).ThenInclude(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Flights).ThenInclude(x => x.Flight).ThenInclude(x => x.Destination).ThenInclude(x => x.City)
                .ToListAsync();

            return bookings;
        }

        public async Task<Booking> Create(Payment payment)
        {
            Booking booking = new Booking
            {
                Payment = payment,
                PaymentId = payment.Id,
                Id = Guid.NewGuid().ToString(),
                TotalPrice = payment.Amount
            };

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task Delete(string id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            booking.IsDeleted = true;

            var flights = booking.Flights.Select(x => x.Flight);

            //make the seats available
            foreach (var flight in flights)
            {
                var seats = flight.Passengers.Select(x => x.SeatNumber).ToList();

                flight.Seats.Where(x => seats.Contains(x.SeatNumber)).First().IsAvailable = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(string id)
        {
            return await _context.Bookings.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings
                .Include(x => x.Payment)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Origin)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Destination)
                //.Include(x => x.Flights)
                //    .ThenInclude(x => x.Flight)
                //        .ThenInclude(x => x.Aircraft)
                //.Include(x => x.Flights)
                //    .ThenInclude(x => x.Flight)
                //        .ThenInclude(x => x.Passengers)
                //            .ThenInclude(x => x.Person)
                //                .ThenInclude(x => x.Services)
                .Include(x => x.Passengers)
                    .ThenInclude(x => x.Passenger)
                //.ThenInclude(x => x.Services)
                //.ThenInclude(x => x.Service)
                .AsSplitQuery()
                .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Booking> GetById(string id)
        {
            return await _context.Bookings
                .Include(x => x.Payment)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Origin)
                            .ThenInclude(x => x.City)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Destination)
                            .ThenInclude(x => x.City)
                //.Include(x => x.Flights)
                //    .ThenInclude(x => x.Flight)
                //        .ThenInclude(x => x.Aircraft)
                //.Include(x => x.Flights)
                //    .ThenInclude(x => x.Flight)
                //        .ThenInclude(x => x.Passengers)
                //            .ThenInclude(x => x.Person)
                //                .ThenInclude(x => x.Services)
                .Include(x => x.Passengers)
                    .ThenInclude(x => x.Passenger)
                        .ThenInclude(x => x.Services)
                            .ThenInclude(x => x.Service)
                .AsSplitQuery()
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
