namespace BuzzAir.Models.EditModels
{
    public class FlightEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string OriginCountry { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string DestinationCountry { get; set; } = string.Empty;
        public string Aircraft { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public DateTime Departure { get; set; }
        public decimal Price { get; set; }
        public ICollection<SelectListItem> OriginOptions { get; set; } = [];
        public ICollection<SelectListItem> OriginAirportsOptions { get; set; } = [];
        public ICollection<SelectListItem> DestinationOptions { get; set; } = [];
        public ICollection<SelectListItem> DestinationAirportsOptions { get; set; } = [];
        public ICollection<SelectListItem> AircraftOptions { get; set; } = [];
    }
}
