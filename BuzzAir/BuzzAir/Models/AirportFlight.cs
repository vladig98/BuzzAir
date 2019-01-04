namespace BuzzAir.Models
{
    public class AirportFlight
    {
        public int Id { get; set; }

        public int AirportId { get; set; }
        public Airport Airport { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public AirportType Type { get; set; }
    }
}
