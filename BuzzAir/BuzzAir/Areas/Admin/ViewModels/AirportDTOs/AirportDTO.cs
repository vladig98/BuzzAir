namespace BuzzAir.Areas.Admin.ViewModels.AirportDTOs
{
    public record class AirportDTO(
        string Id, 
        string ICAO, 
        string IATA, 
        string Name, 
        string City, 
        string State,
        string Country, 
        int Elevation,
        double Latitude, 
        double Longitude, 
        string Timezone
    );
}
