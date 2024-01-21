namespace BuzzAir.Models.ViewModels
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            Passengers = new List<PassengerViewModel>();
        }

        public FlightViewModel Outbound { get; set; }

        public FlightViewModel Inbound { get; set; }

        public string Amount { get; set; }

        public string Id { get; set; }

        public string Currency { get; set; }

        public List<PassengerViewModel> Passengers { get; set; }

        public string User { get; set; }
    }
}
