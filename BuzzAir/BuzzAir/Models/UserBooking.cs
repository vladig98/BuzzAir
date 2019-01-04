using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class UserBooking
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int BookingId { get; set; }
        [Required]
        public Booking Booking { get; set; }
    }
}
