namespace BuzzAir.Models.CreateModels
{
    public record AirportCreateViewModel
    {
        [Required]
        [Display(Prompt = nameof(ICAO))]
        public string ICAO { get; set; } = string.Empty;
        [Display(Prompt = nameof(IATA))]
        public string IATA { get; set; } = string.Empty;
        [Required]
        [Display(Prompt = nameof(Name))]
        public string Name { get; set; } = string.Empty;
        [Display(Prompt = nameof(City))]
        public string City { get; set; } = string.Empty;
        [Display(Prompt = nameof(State))]
        public string State { get; set; } = string.Empty;
        [Required]
        [Display(Prompt = nameof(Country))]
        public string Country { get; set; } = string.Empty;
        [Required]
        [Display(Prompt = nameof(Elevation))]
        public int Elevation { get; set; }
        [Required]
        [Display(Prompt = nameof(Latitude))]
        public double Latitude { get; set; }
        [Required]
        [Display(Prompt = nameof(Longitude))]
        public double Longitude { get; set; }
        [Required]
        [Display(Prompt = nameof(TimeZone))]
        public string TimeZone { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> CountryOptions { get; set; } = [];
        public IEnumerable<SelectListItem> TimezoneOptions { get; set; } = [];
        public IEnumerable<City> Cities { get; set; } = [];
        public IEnumerable<State> States { get; set; } = [];
    }
}
