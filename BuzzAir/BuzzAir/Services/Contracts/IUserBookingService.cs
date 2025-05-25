namespace BuzzAir.Services.Contracts
{
    public interface IUserBookingService
    {
        Task<IEnumerable<UserBooking>> GetAll();
        Task<IEnumerable<UserBooking>> GetAllForUser(string username);
        Task CreateAsync(ApplicationUser currentUser, Booking booking);
    }
}
