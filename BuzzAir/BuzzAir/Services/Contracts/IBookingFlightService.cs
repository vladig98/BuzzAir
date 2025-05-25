namespace BuzzAir.Services.Contracts
{
    public interface IBookingFlightService
    {
        Task CreateAsync(Booking booking, Flight? outbound, bool isOutbound = false);
    }
}
