namespace BuzzAir.Models.DbModels
{
    public class Airport
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string ICAO { get; set; }
        public required string IATA { get; set; }
        public required string Name { get; set; }

        public required City City { get; set; }
        public required string CityId { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public int? ElevationAboveSeaLevel { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Flight> FlightsFrom { get; set; } = new HashSet<Flight>();
        public ICollection<Flight> FlightsTo { get; set; } = new HashSet<Flight>();
    }
}