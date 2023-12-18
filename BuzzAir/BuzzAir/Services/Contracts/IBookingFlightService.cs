using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IBookingFlightService
    {
        Task<BookingFlight> Create(Flight flight, Booking booking, bool isOutbound = false);
    }
}
