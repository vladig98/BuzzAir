namespace BuzzAir.Data.Models;

public class Airport
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ICAO { get; set; } = string.Empty;
    public string IATA { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public City City { get; set; } = null!;
    public string CityId { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

    public int? ElevationAboveSeaLevel { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Flight> FlightsFrom { get; } = new HashSet<Flight>();
    public ICollection<Flight> FlightsTo { get; } = new HashSet<Flight>();
}