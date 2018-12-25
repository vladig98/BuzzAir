using System.Collections.Generic;
using System.Linq;

namespace BuzzAir.Models
{
    public class IndexModel
    {
        public IndexModel()
        {
            this.Airports = new List<Airport>();
            this.Flights = new List<Flight>();
        }

        public ICollection<Airport> Airports { get; set; }

        public ICollection<Flight> Flights { get; set; }

        public string Selected { get; set; }

        public string Greeting { get; set; }

        public bool Authenticated { get; set; }
    }
}
