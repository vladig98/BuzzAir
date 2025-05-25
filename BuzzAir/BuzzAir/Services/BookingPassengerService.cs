namespace BuzzAir.Services
{
    public class BookingPassengerService(BuzzAirDbContext context) : IBookingPassengerService
    {
        public async Task<BookingPassenger> Create(Booking booking, Person person)
        {
            BookingPassenger bookingPassenger = BookingFactory.CreatePassengerForBooking(booking, person);

            await context.BookingPassengers.AddAsync(bookingPassenger);
            await context.SaveChangesAsync();

            return bookingPassenger;
        }

        public async Task CreateAsync(List<IPassenger> passengers, Booking booking)
        {
            List<Task> tasks = [];

            foreach (IPassenger passenger in passengers)
            {
                Task passengerTask = Create(booking, (Passenger)passenger);

                tasks.Add(passengerTask);
            }

            await Task.WhenAll(tasks);
        }
    }
}
