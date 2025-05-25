namespace BuzzAir.Models.CreateModels
{
    public class CreateFlightViewModel
    {
        public CreateFlightViewModel()
        {
            Aircrafts = new List<SelectListItem>();
            Airports = new List<SelectListItem>();
            Price = 9.99M;
            DurationInMinutes = 30;
            Departure = DateTime.UtcNow;
            OriginCountryOptions = new List<SelectListItem>();
            DestinationCountryOptions = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = nameof(FlightNumber), Prompt = nameof(FlightNumber))]
        [RegularExpression("[A-Z]{2,2}-\\d{4,4}", ErrorMessage = "The flight number is invalid")]
        public string FlightNumber { get; set; }

        [Required]
        public string Aircraft { get; set; }

        [Required]
        [Range(30, 1440)]
        [Display(Name = nameof(DurationInMinutes), Prompt = nameof(DurationInMinutes))]
        public int DurationInMinutes { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        [Range(9.99, double.MaxValue)]
        public decimal Price { get; set; }

        public List<SelectListItem> Aircrafts { get; set; }

        public List<SelectListItem> Airports { get; set; }

        public Country OriginCountry { get; set; }
        public Country DestinationCountry { get; set; }

        public IEnumerable<SelectListItem> OriginCountryOptions { get; set; }
        public IEnumerable<SelectListItem> DestinationCountryOptions { get; set; }
    }
}
