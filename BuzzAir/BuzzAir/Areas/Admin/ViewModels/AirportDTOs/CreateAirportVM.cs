namespace BuzzAir.Areas.Admin.ViewModels.AirportDTOs
{
    public class CreateAirportVM
    {
        [Required]
        public string ICAO { get; set; } = string.Empty;
        public string IATA { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CityId { get; set; } = string.Empty;
        [HiddenInput]
        public City? City { get; set; }
        public string StateId { get; set; } = string.Empty;
        [HiddenInput]
        public State? State { get; set; }
        [Required]
        public string CountryId { get; set; } = string.Empty;
        [HiddenInput]
        public Country? Country { get; set; }
        [Required]
        public int Elevation { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string TimezoneId { get; set; } = string.Empty;
        [HiddenInput]
        public string Timezone { get; set; } = string.Empty;
        public List<SelectListItem> CountryOptions { get; set; } = [];
        public List<SelectListItem> TimezoneOptions { get; set; } = [];
    }
}
