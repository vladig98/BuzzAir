namespace BuzzAir.Areas.Admin.ViewModels.AirportDTOs
{
    public class EditAirportVM
    {
        [Required]
        [HiddenInput]
        public string Id { get; set; } = string.Empty;
        public string ICAO { get; set; } = string.Empty;
        public string IATA { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [HiddenInput]
        public string CityId { get; set; } = string.Empty;
        [HiddenInput]
        public City? City { get; set; }
        [HiddenInput]
        public string StateId { get; set; } = string.Empty;
        [HiddenInput]
        public State? State { get; set; }
        [HiddenInput]
        public string CountryId { get; set; } = string.Empty;
        [HiddenInput]
        public Country? Country { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public int Elevation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimezoneName { get; set; } = string.Empty;
        [HiddenInput]
        public string TimezoneId { get; set; } = string.Empty;
        [HiddenInput]
        public string Timezone { get; set; } = string.Empty;
        public List<SelectListItem> CountryOptions { get; set; } = [];
        public List<SelectListItem> TimezoneOptions { get; set; } = [];
    }
}
