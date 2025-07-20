namespace BuzzAir.Areas.Admin.ViewModels.AirportDTOs
{
    public class DeleteAirportVM
    {
        [Required]
        [HiddenInput]
        public string Id { get; set; } = string.Empty;
        public string ICAO { get; set; } = string.Empty;
        public string IATA { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Elevation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Timezone { get; set; } = string.Empty;
    }
}
