using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services.Contracts
{
    public interface IBookingPassengerService
    {
        Task<BookingPassenger> Create(Booking booking, Person person);
        Task CreateAsync(List<IPassenger> passengers, Booking booking);
    }
}
