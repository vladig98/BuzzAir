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
        public Booking Booking { get; set; }

        [Required]
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
    }
}
