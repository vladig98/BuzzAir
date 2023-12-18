using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class FlightPassenger
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }

        [Required]
        public string PersonId { get; set; }
        [Required]
        public Person Person { get; set; }

        [Required]
        public int SeatNumber { get; set; }
    }
}
