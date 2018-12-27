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
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public decimal TotalPrice => (this.Flights.Sum(x => x.Flight.Price) + this.Passengers.Sum(x => x.Person.Services.Sum(y => y.Service.Price))) * this.Passengers.Count;

        [Required]
        public virtual ICollection<BookingFlight> Flights { get; set; }

        [Required]
        public virtual ICollection<BookingPassenger> Passengers { get; set; }

        [Required]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
