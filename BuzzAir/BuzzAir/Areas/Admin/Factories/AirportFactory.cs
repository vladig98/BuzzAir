namespace BuzzAir.Areas.Admin.Factories;

public static class AirportFactory
{
    public static DeleteAirportVM BuildDeleteAirportVM(AirportDTO airport)
    {
        ArgumentNullException.ThrowIfNull(airport);

        DeleteAirportVM model = new()
        {
            Id = airport.Id,
            IATA = airport.IATA,
            ICAO = airport.ICAO,
            Name = airport.Name,
            Latitude = airport.Latitude,
            Longitude = airport.Longitude,
            ElevationAboveSeaLevel = airport.ElevationAboveSeaLevel,
            CityName = airport.City
        };

        return model;
    }

    public static EditAirportVM BuildEditAirportVM(AirportDTO airport, IEnumerable<CityDTO> cities)
    {
        ArgumentNullException.ThrowIfNull(airport);
        ArgumentNullException.ThrowIfNull(cities);

        EditAirportVM model = new()
        {
            Id = airport.Id,
            IATA = airport.IATA,
            ICAO = airport.ICAO,
            Name = airport.Name,
            Latitude = airport.Latitude,
            Longitude = airport.Longitude,
            ElevationAboveSeaLevel = airport.ElevationAboveSeaLevel,
            CityId = airport.CityId
        };

        foreach (CityDTO city in cities)
        {
            SelectListItem item = new(city.Name, city.Id);
            model.CityOptions.Add(item);
        }

        return model;
    }

    public static RestoreAirportVM BuildRestoreAirportVM(AirportDTO airport)
    {
        ArgumentNullException.ThrowIfNull(airport);

        RestoreAirportVM model = new()
        {
            Id = airport.Id,
            IATA = airport.IATA,
            ICAO = airport.ICAO,
            Name = airport.Name,
            Latitude = airport.Latitude,
            Longitude = airport.Longitude,
            ElevationAboveSeaLevel = airport.ElevationAboveSeaLevel,
            CityName = airport.City
        };

        return model;
    }

    public static CreateAirportVM BuildCreateAirportVM(IEnumerable<CityDTO> cities)
    {
        ArgumentNullException.ThrowIfNull(cities);
        CreateAirportVM model = new();

        foreach (CityDTO city in cities)
        {
            SelectListItem item = new(city.Name, city.Id);
            model.CityOptions.Add(item);
        }

        return model;
    }
}
