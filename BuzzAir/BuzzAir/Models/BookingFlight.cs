using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class BookingFlight
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }
        [Required]
        public virtual Booking Booking { get; set; }

        [Required]
        public int FlightId { get; set; }
        [Required]
        public virtual Flight Flight { get; set; }
    }
}
