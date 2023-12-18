using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuzzAir.Services
{
    public class UserBookingService : IUserBookingService
    {
        private readonly BuzzAirDbContext _context;

        public UserBookingService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<UserBooking> Create(ApplicationUser user, Booking booking)
        {
            UserBooking userBooking = new UserBooking
            {
                ApplicationUserId = user.Id,
                BookingId = booking.Id,
                Id = Guid.NewGuid().ToString()
            };

            await _context.UserBookings.AddAsync(userBooking);
            await _context.SaveChangesAsync();

            return userBooking;
        }

        public async Task<IEnumerable<UserBooking>> GetAll()
        {
            return await _context.UserBookings.Where(x => !x.Booking.Deleted).Include(x => x.Booking).Include(x => x.ApplicationUser).AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<UserBooking>> GetAllForUser(string username)
        {
            return await _context.UserBookings.Where(x => x.ApplicationUser.UserName == username).Where(x => !x.Booking.Deleted).Include(x => x.Booking).Include(x => x.ApplicationUser).AsSplitQuery().ToListAsync();
        }
    }
}
