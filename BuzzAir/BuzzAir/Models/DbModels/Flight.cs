namespace BuzzAir.Models.DbModels
{
    public class Flight
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string FlightNumber { get; set; } = string.Empty;
        public string OriginId { get; set; } = string.Empty;
        [JsonIgnore]
        [Required]
        public Airport Origin { get; set; } = new Airport();
        public string DestinationId { get; set; } = string.Empty;
        [JsonIgnore]
        [Required]
        public Airport Destination { get; set; } = new Airport();
        [Required]
        public string AircraftId { get; set; } = string.Empty;
        [JsonIgnore]
        [Required]
        public Aircraft Aircraft { get; set; } = new Aircraft();
        [Required]
        public int DurationInMinutes { get; set; }
        [Required]
        public DateTime Departure { get; set; }
        [Required]
        public DateTime Arrival { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int TakenSeats => Passengers.Count;
        public ICollection<FlightPassenger> Passengers { get; set; } = [];
        public ICollection<FlightSeat> Seats { get; set; } = [];
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return $"[{Departure.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture)}] {Origin.IATA} -> " +
                $"{Destination.IATA} [{Arrival.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture)}] ${Price}";
        }
    }
}