namespace BuzzAir.Services.Interfaces
{
    public interface IBookingFlightService
    {
        Task CreateAsync(Booking booking, Flight? outbound, bool isOutbound = false);
    }
}
