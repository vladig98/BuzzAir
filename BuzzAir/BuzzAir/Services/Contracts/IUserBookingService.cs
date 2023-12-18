using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface IUserBookingService
    {
        Task<IEnumerable<UserBooking>> GetAll();
        Task<IEnumerable<UserBooking>> GetAllForUser(string username);
        Task<UserBooking> Create(ApplicationUser user, Booking booking);
    }
}
