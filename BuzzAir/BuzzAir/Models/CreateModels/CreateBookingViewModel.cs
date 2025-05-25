namespace BuzzAir.Models.CreateModels
{
    public class CreateBookingViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PassengersCount { get; set; }
        public List<PassengerViewModel> Passengers { get; set; } = [];
        public List<FlightViewModel> OutboundFlights { get; set; } = [];
        public List<FlightViewModel> InboundFlights { get; set; } = [];
        public string? GoingFlightSelection { get; set; }
        public string? ReturnFlightSelection { get; set; }
        [HiddenInput(DisplayValue = false)]
        public decimal Price { get; set; }
        public PaymentViewModel Payment { get; set; } = new PaymentViewModel();
    }
}
