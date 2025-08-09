namespace BuzzAir.Areas.Admin.ViewModels.FlightViewModels;

public class EditFlightVM
{
    public string Id { get; set; } = string.Empty;

    public string FlightNumber { get; set; } = string.Empty;

    public string OriginId { get; set; } = string.Empty;
    public string DestinationId { get; set; } = string.Empty;

    public string AircraftId { get; set; } = string.Empty;

    public DateTime DepartureUTC { get; set; }
    public DateTime ArrivalUTC { get; set; }

    public decimal PriceInEur { get; set; }

    public ICollection<SelectListItem> Aircraft { get; } = [];
    public ICollection<SelectListItem> OriginAirports { get; } = [];
    public ICollection<SelectListItem> DestinationAirports { get; } = [];
}
