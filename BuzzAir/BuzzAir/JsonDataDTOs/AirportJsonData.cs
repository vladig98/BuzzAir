namespace BuzzAir.JsonDataDTOs;

public class AirportJsonData
{
    public string Icao { get; set; } = string.Empty;
    public string Iata { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Elevation { get; set; }
    public decimal Lat { get; set; }
    public decimal Lon { get; set; }
    public string Tz { get; set; } = string.Empty;
}
