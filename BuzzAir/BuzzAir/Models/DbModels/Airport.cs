namespace BuzzAir.Models.DbModels
{
    public class Airport
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ICAO { get; set; } = string.Empty;
        public string IATA { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public City City { get; set; } = new City();
        public string CityId { get; set; } = string.Empty;
        public State? State { get; set; }
        public string? StateId { get; set; }
        [Required]
        public Country Country { get; set; } = new Country();
        public string CountryId { get; set; } = string.Empty;
        [Required]
        public int Elevation { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string TimeZone { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public ICollection<Flight> FlightsFromOrigin { get; set; } = [];
        public ICollection<Flight> FlightsToDestination { get; set; } = [];
    }
}