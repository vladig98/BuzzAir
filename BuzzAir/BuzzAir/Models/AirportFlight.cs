namespace BuzzAir.Models
{
    public class AirportFlight
    {
        public int Id { get; set; }

        public int AirportId { get; set; }
        public virtual Airport Airport { get; set; }

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        public virtual AirportType Type { get; set; }
    }
}
