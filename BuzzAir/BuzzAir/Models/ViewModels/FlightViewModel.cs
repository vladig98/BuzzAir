namespace BuzzAir.Models.ViewModels
{
    public record FlightViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; } = string.Empty;
        public AirportViewModel Origin { get; set; } = new AirportViewModel();
        public AirportViewModel Destination { get; set; } = new AirportViewModel();
        [HiddenInput(DisplayValue = false)]
        public DateTime Departure { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime Arrival { get; set; }
        [HiddenInput(DisplayValue = false)]
        public decimal Price { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string FlightNumber { get; set; } = string.Empty;
        [HiddenInput(DisplayValue = false)]
        public bool IsOutbound { get; set; }
    }
}
