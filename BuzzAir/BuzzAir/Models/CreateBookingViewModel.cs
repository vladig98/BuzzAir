using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Models
{
    public class CreateBookingViewModel
    {
        public CreateBookingViewModel()
        {
            Flights = new Dictionary<string, List<FlightViewModel>>();
            Passengers = new List<PassengerViewModel>();
        }

        [HiddenInput(DisplayValue = false)]
        public int PassengersCount { get; set; }
        public List<PassengerViewModel> Passengers { get; set; }

        public Dictionary<string, List<FlightViewModel>> Flights { get; set; }
        public string? GoingFlightSelection { get; set; }
        public string? ReturnFlightSelection { get; set; }

        [HiddenInput(DisplayValue = false)]
        public decimal Price { get; set; }

        public PaymentViewModel Payment { get; set; }
    }
}
