namespace BuzzAir.Models.EditModels
{
    public class FlightEditViewModel
    {
        public FlightEditViewModel()
        {
            OriginOptions = new List<SelectListItem>();
            DestinationOptions = new List<SelectListItem>();
            AircraftOptions = new List<SelectListItem>();
            OriginAirportsOptions = new List<SelectListItem>();
            DestinationAirportsOptions = new List<SelectListItem>();
        }

        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string OriginCountry { get; set; }
        public string Destination { get; set; }
        public string DestinationCountry { get; set; }
        public string Aircraft { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime Departure { get; set; }
        public decimal Price { get; set; }

        public ICollection<SelectListItem> OriginOptions { get; set; }
        public ICollection<SelectListItem> OriginAirportsOptions { get; set; }
        public ICollection<SelectListItem> DestinationOptions { get; set; }
        public ICollection<SelectListItem> DestinationAirportsOptions { get; set; }
        public ICollection<SelectListItem> AircraftOptions { get; set; }
    }
}
