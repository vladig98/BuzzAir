namespace BuzzAir.Areas.Admin.Factories;

public static class FlightFactory
{
    public static CreateFlightVM BuildCreateFlightVM(IEnumerable<AirportDTO> airports, IEnumerable<AircraftDTO> aircraft)
    {
        ArgumentNullException.ThrowIfNull(airports);
        ArgumentNullException.ThrowIfNull(aircraft);

        CreateFlightVM model = new();

        foreach (AirportDTO airport in airports)
        {
            SelectListItem itemOrigin = new(airport.Name, airport.Id);
            SelectListItem itemDestination = new(airport.Name, airport.Id);

            model.OriginAirports.Add(itemOrigin);
            model.DestinationAirports.Add(itemDestination);
        }

        foreach (AircraftDTO aircraftModel in aircraft)
        {
            SelectListItem item = new(aircraftModel.Name, aircraftModel.Id);
            model.Aircraft.Add(item);
        }

        return model;
    }

    public static DeleteFlightVM BuildDeleteFlightVM(FlightDTO flight)
    {
        ArgumentNullException.ThrowIfNull(flight);

        DeleteFlightVM model = new()
        {
            AircraftModel = flight.AircraftModel,
            ArrivalUTC = flight.ArrivalUTC,
            DepartureUTC = flight.DepartureUTC,
            DestinationName = flight.Destination,
            FlightNumber = flight.FlightNumber,
            Id = flight.Id,
            OriginName = flight.Origin,
            PriceInEur = flight.PriceInEur
        };

        return model;
    }

    public static EditFlightVM BuildEditFlightVM(FlightDTO flight, IEnumerable<AirportDTO> airports, IEnumerable<AircraftDTO> aircraft)
    {
        ArgumentNullException.ThrowIfNull(airports);
        ArgumentNullException.ThrowIfNull(aircraft);
        ArgumentNullException.ThrowIfNull(flight);

        EditFlightVM model = new()
        {
            AircraftId = flight.AircraftModelId,
            ArrivalUTC = flight.ArrivalUTC,
            DepartureUTC = flight.DepartureUTC,
            DestinationId = flight.DestinationId,
            FlightNumber = flight.FlightNumber,
            Id = flight.Id,
            OriginId = flight.OriginId,
            PriceInEur = flight.PriceInEur
        };

        foreach (AirportDTO airport in airports)
        {
            SelectListItem itemOrigin = new(airport.Name, airport.Id);
            SelectListItem itemDestination = new(airport.Name, airport.Id);

            model.OriginAirports.Add(itemOrigin);
            model.DestinationAirports.Add(itemDestination);
        }

        foreach (AircraftDTO aircraftModel in aircraft)
        {
            SelectListItem item = new(aircraftModel.Name, aircraftModel.Id);
            model.Aircraft.Add(item);
        }

        return model;
    }

    public static RestoreFlightVM BuildRestoreFlightVM(FlightDTO flight)
    {
        ArgumentNullException.ThrowIfNull(flight);

        RestoreFlightVM model = new()
        {
            AircraftModel = flight.AircraftModel,
            ArrivalUTC = flight.ArrivalUTC,
            DepartureUTC = flight.DepartureUTC,
            DestinationName = flight.Destination,
            FlightNumber = flight.FlightNumber,
            Id = flight.Id,
            OriginName = flight.Origin,
            PriceInEur = flight.PriceInEur
        };

        return model;
    }
}
