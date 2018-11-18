using System.Collections.Generic;
using System.Linq;

namespace BuzzAir.Models
{
    public class Booking
    {
        public Booking()
        {
            this.Flights = new HashSet<Flight>();
            this.Passengers = new HashSet<Person>();
        }

        public int Id { get; set; }

        public decimal TotalPrice => (this.Flights.Sum(x => x.Price) * this.Passengers.Count) + this.Passengers.Sum(x => x.Services.Sum(y => y.Price));

        public virtual ICollection<Flight> Flights { get; set; }

        public virtual ICollection<Person> Passengers { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
