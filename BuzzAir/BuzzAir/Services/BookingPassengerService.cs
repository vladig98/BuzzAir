using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class BookingPassengerService : IBookingPassengerService
    {
        private readonly BuzzAirDbContext _context;

        public BookingPassengerService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<BookingPassenger> Create(Booking booking, Person person)
        {
            BookingPassenger bookingPassenger = new BookingPassenger()
            {
                BookingId = booking.Id,
                PassengerId = person.Id,
                Id = Guid.NewGuid().ToString()
            };

            await _context.BookingPassengers.AddAsync(bookingPassenger);
            await _context.SaveChangesAsync();

            return bookingPassenger;
        }
    }
}
