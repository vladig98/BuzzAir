namespace BuzzAir.Models
{
    public class CreateFlightViewModel
    {
        public string FlightNumber { get; set; }

        public string Aircraft { get; set; }

        public string DurationInMinutes { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public string Price { get; set; }
    }
}
