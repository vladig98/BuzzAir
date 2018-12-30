using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class CreateFlightIndexViewModel
    {
        public CreateFlightIndexViewModel()
        {
            this.Aircrafts = new List<Aircraft>();
            this.Airports = new List<Airport>();
        }

        public List<Aircraft> Aircrafts { get; set; }

        public List<Airport> Airports { get; set; }
    }
}
