namespace BuzzAir.Areas.Admin.ViewModels.AirportViewModels;

public class CreateAirportVM
{
    public string ICAO { get; set; } = string.Empty;
    public string IATA { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public int? ElevationAboveSeaLevel { get; set; }
    public string CityId { get; set; } = string.Empty;
    public ICollection<SelectListItem> CityOptions { get; } = [];
}
