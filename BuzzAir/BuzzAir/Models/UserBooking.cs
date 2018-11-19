namespace BuzzAir.Models
{
    public class UserBooking
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
