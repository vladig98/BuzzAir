using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class FlightPassenger
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }

        [Required]
        public int PersonId { get; set; }
        [Required]
        public Person Person { get; set; }
    }
}
