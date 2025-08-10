namespace BuzzAir.Areas.Admin.DTOs;

public sealed record class AirportDTO(
    string Id,
    string Name,
    string ICAO,
    string IATA,
    string City,
    string CityId,
    decimal? Latitude,
    decimal? Longitude,
    int? ElevationAboveSeaLevel
);
