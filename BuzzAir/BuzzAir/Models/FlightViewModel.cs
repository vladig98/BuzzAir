using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Models
{
    public class FlightViewModel
    {
        public FlightViewModel()
        {
            IsOutbound = false;
        }

        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public AirportViewModel Origin { get; set; }
        public AirportViewModel Destination { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Departure { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Arrival { get; set; }

        [HiddenInput(DisplayValue = false)]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FlightNumber { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsOutbound { get; set; }
    }
}
