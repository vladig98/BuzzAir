namespace BuzzAir.ViewModels;

public class CreateBookingViewModel
{
    public IList<SelectListItem> Outbound { get; } = [];
    public IList<SelectListItem> Inbound { get; } = [];
    public int PassengersCount { get; set; }
    public IList<PassengerViewModel> Passengers { get; } = [];
    public string OutboundId { get; set; } = string.Empty;
    public string? InboundId { get; set; }
    public PaymentViewModel Payment { get; set; } = null!;
}
