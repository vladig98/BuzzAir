using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IBookingPassengerService
    {
        Task<BookingPassenger> Create(Booking booking, Person person);
    }
}
