namespace BuzzAir.Models.DbModels
{
    public class Flight
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string FlightNumber { get; set; }

        public required string OriginId { get; set; }
        public required Airport Origin { get; set; }

        public required string DestinationId { get; set; }
        public required Airport Destination { get; set; }

        public required string AircraftId { get; set; }
        public required Aircraft Aircraft { get; set; }

        public DateTime DepartureUTC { get; set; }
        public DateTime ArrivalUTC { get; set; }

        public decimal PriceInEur { get; set; }
        public int TakenSeats { get; private set; }

        public bool IsDeleted { get; set; }

        public ICollection<FlightPassenger> Passengers { get; set; } = new HashSet<FlightPassenger>();
        public ICollection<BookingFlight> Bookings { get; set; } = new HashSet<BookingFlight>();

        public int DurationInMinutes
            => (int)(ArrivalUTC - DepartureUTC).TotalMinutes;

        public int AvailableSeats
            => Aircraft?.NumberOfSeats - TakenSeats ?? 0;
    }
}