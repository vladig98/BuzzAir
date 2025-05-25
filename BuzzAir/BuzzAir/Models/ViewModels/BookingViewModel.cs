namespace BuzzAir.Models.ViewModels
{
    public record BookingViewModel
    {
        public FlightViewModel Outbound { get; set; } = new FlightViewModel();
        public FlightViewModel Inbound { get; set; } = new FlightViewModel();
        public string Amount { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public List<PassengerViewModel> Passengers { get; set; } = [];
        public string User { get; set; } = string.Empty;
    }
}
