using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class UserBooking
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int AppUserId { get; set; }
        [Required]
        public virtual AppUser AppUser { get; set; }

        [Required]
        public int BookingId { get; set; }
        [Required]
        public virtual Booking Booking { get; set; }
    }
}
