using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class UserBooking
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int BookingId { get; set; }
        [Required]
        public virtual Booking Booking { get; set; }
    }
}
