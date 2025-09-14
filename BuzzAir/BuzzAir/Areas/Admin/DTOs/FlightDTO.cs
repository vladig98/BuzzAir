namespace BuzzAir.Areas.Admin.DTOs;

public sealed record class FlightDTO(
    string Id,
    string FlightNumber,
    string Origin,
    string OriginId,
    string Destination,
    string DestinationId,
    string AircraftModel,
    string AircraftModelId,
    DateTime DepartureUTC,
    DateTime ArrivalUTC,
    decimal PriceInEur,
    int TakenSeats
);