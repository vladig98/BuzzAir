using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IBookingService
    {
        Task<bool> ExistsById(string id);
        Task<Booking> GetById(string id);
        Task Delete(string id);
        Task<IEnumerable<Booking>> GetAll();
        Task<Booking> Create(Payment payment);
        Task<IEnumerable<Booking>> GetAllForUser(IEnumerable<UserBooking> userBookings);
    }
}
