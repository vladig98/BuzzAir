using System.Collections.Generic;
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

        public int Id { get; set; }

        public decimal TotalPrice => (this.Flights.Sum(x => x.Flight.Price) + this.Passengers.Sum(x => x.Person.Services.Sum(y => y.Service.Price))) * this.Passengers.Count;

        public virtual ICollection<BookingFlight> Flights { get; set; }

        public virtual ICollection<BookingPassenger> Passengers { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
