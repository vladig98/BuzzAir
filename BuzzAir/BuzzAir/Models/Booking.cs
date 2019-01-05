using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BuzzAir.Models
{
    public class Booking
    {
        public Booking()
        {
            this.Flights = new HashSet<BookingFlight>();
            this.Passengers = new HashSet<BookingPassenger>();
            this.Deleted = false;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public decimal TotalPrice => (this.Flights.Sum(x => x.Flight.Price) + this.Passengers.Sum(x => x.Person.Services.Sum(y => y.Service.Price))) * this.Passengers.Count;

        [Required]
        public ICollection<BookingFlight> Flights { get; set; }

        [Required]
        public ICollection<BookingPassenger> Passengers { get; set; }

        [Required]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public bool Deleted { get; set; }
    }
}
