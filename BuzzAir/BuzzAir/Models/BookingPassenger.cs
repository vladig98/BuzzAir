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
        public virtual Booking Booking { get; set; }

        [Required]
        public int PersonId { get; set; }
        [Required]
        public virtual Person Person { get; set; }
    }
}
