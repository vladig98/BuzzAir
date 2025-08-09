namespace BuzzAir.Areas.Admin.ViewModels.FlightViewModels;

public class CreateFlightVM
{
    public string FlightNumber { get; set; } = string.Empty;

    public string OriginId { get; set; } = string.Empty;
    public string DestinationId { get; set; } = string.Empty;

    public string AircraftId { get; set; } = string.Empty;

    public DateTime DepartureUTC { get; set; } = DateTime.UtcNow;
    public DateTime ArrivalUTC { get; set; } = DateTime.UtcNow.AddHours(1);

    public decimal PriceInEur { get; set; }

    public ICollection<SelectListItem> Aircraft { get; } = [];
    public ICollection<SelectListItem> OriginAirports { get; } = [];
    public ICollection<SelectListItem> DestinationAirports { get; } = [];
}
