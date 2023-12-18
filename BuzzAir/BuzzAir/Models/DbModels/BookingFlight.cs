using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class BookingFlight
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string BookingId { get; set; }
        [Required]
        public Booking Booking { get; set; }

        [Required]
        public string FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }

        public bool IsOutbound { get; set; }
    }
}
