namespace BuzzAir.Services.Interfaces
{
    public interface IBookingPassengerService
    {
        Task<BookingPassenger> Create(Booking booking, Person person);
        Task CreateAsync(List<IPassenger> passengers, Booking booking);
    }
}
