namespace BuzzAir.Models.DTOs
{
    public record FlightInfo
    {
        public string CountryName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string CityId { get; set; } = string.Empty;
    }
}
