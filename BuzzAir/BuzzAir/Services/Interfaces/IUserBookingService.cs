namespace BuzzAir.Services.Interfaces
{
    public interface IUserBookingService
    {
        Task<IEnumerable<UserBooking>> GetAllForUser(string username);
        Task CreateAsync(ApplicationUser currentUser, Booking booking);
    }
}
