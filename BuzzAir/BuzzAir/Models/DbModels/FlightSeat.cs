using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class FlightSeat
    {
        public string Id { get; set; }

        [Required]
        public string FlightId { get; set; }
        public Flight Flight { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;
    }
}
