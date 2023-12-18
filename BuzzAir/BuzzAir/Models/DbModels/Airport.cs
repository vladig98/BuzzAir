using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Airport
    {
        public Airport()
        {
            FlightsFromOrigin = new HashSet<Flight>();
            FlightsToDestination = new HashSet<Flight>();
        }

        public string Id { get; set; }

        [Required]
        public string ICAO { get; set; }

        public string IATA { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public City City { get; set; }
        public string CityId { get; set; }

        public State? State { get; set; }
        public string? StateId { get; set; }

        [Required]
        public Country Country { get; set; }
        public string CountryId { get; set; }

        [Required]
        public int Elevation { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string TimeZone { get; set; }

        public ICollection<Flight> FlightsFromOrigin { get; set; }
        public ICollection<Flight> FlightsToDestination { get; set; }
    }
}