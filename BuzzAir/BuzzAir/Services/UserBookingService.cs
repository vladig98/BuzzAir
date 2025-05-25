namespace BuzzAir.Services
{
    public class UserBookingService(BuzzAirDbContext context) : IUserBookingService
    {
        public async Task CreateAsync(ApplicationUser currentUser, Booking booking)
        {
            UserBooking userBooking = BookingFactory.CreateBookingForAUser(booking, currentUser);

            await context.UserBookings.AddAsync(userBooking);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserBooking>> GetAllForUser(string username)
        {
            return await context.UserBookings
                .Where(x => x.ApplicationUser.UserName == username)
                .Where(x => !x.Booking.IsDeleted)
                .Include(x => x.Booking)
                .Include(x => x.ApplicationUser)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
