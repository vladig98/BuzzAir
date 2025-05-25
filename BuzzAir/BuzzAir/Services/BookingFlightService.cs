namespace BuzzAir.Services
{
    public class BookingFlightService(BuzzAirDbContext context) : IBookingFlightService
    {
        public async Task CreateAsync(Booking booking, Flight? flight, bool isOutbound = false)
        {
            if (flight == null)
            {
                return;
            }

            BookingFlight bookingFlight = FlightFactory.CreateFlightForBooking(booking, flight, isOutbound);

            await context.BookingFlights.AddAsync(bookingFlight);
            await context.SaveChangesAsync();
        }
    }
}
