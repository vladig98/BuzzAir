using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class BookingPassenger
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }
        [Required]
        public Booking Booking { get; set; }

        [Required]
        public int PersonId { get; set; }
        [Required]
        public Person Person { get; set; }
    }
}
