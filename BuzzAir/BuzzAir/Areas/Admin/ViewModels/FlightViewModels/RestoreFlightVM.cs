namespace BuzzAir.Areas.Admin.ViewModels.FlightViewModels;

public class RestoreFlightVM
{
    public string Id { get; set; } = string.Empty;

    public string FlightNumber { get; set; } = string.Empty;
    public string OriginName { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public DateTime DepartureUTC { get; set; }
    public DateTime ArrivalUTC { get; set; }
    public string AircraftModel { get; set; } = string.Empty;
    public decimal PriceInEur { get; set; }
}
