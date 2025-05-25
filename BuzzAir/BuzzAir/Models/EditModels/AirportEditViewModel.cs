namespace BuzzAir.Models.EditModels
{
    public class AirportEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public string Name { get; set; }
        public string CityId { get; set; }
        public string? StateId { get; set; }
        public string CountryId { get; set; }
        public int Elevation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimeZone { get; set; }
    }
}
