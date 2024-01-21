using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Booking
    {
        public Booking()
        {
            Flights = new HashSet<BookingFlight>();
            Passengers = new HashSet<BookingPassenger>();
            IsDeleted = false;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public ICollection<BookingFlight> Flights { get; set; }

        [Required]
        public ICollection<BookingPassenger> Passengers { get; set; }

        [Required]
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }

        public bool IsDeleted { get; set; }
    }
}
