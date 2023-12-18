using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace BuzzAir.Models.DbModels
{
    public class Flight
    {
        public Flight()
        {
            Passengers = new HashSet<FlightPassenger>();
            Seats = new List<FlightSeat>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        public string OriginId { get; set; }
        [JsonIgnore]
        [Required]
        public Airport Origin { get; set; }

        public string DestinationId { get; set; }
        [JsonIgnore]
        [Required]
        public Airport Destination { get; set; }

        [Required]
        public string AircraftId { get; set; }
        [JsonIgnore]
        [Required]
        public Aircraft Aircraft { get; set; }

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

        public ICollection<FlightPassenger> Passengers { get; set; }

        public ICollection<FlightSeat> Seats { get; set; }

        public override string ToString()
        {
            return $"[{Departure.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture)}] {Origin.IATA} -> " +
                $"{Destination.IATA} [{Arrival.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture)}] ${Price}";
        }
    }
}