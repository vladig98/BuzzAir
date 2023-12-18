using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class UserBooking
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string BookingId { get; set; }
        [Required]
        public Booking Booking { get; set; }
    }
}
