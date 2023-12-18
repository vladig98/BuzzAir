using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class BookingPassenger
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string BookingId { get; set; }
        [Required]
        public Booking Booking { get; set; }

        [Required]
        public string PassengerId { get; set; }
        [Required]
        public Passenger Passenger { get; set; }
    }
}
