namespace BuzzAir.Data.Models;

public class Flight
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FlightNumber { get; set; } = string.Empty;

    public string OriginId { get; set; } = string.Empty;
    public Airport Origin { get; set; } = null!;

    public string DestinationId { get; set; } = string.Empty;
    public Airport Destination { get; set; } = null!;

    public string AircraftId { get; set; } = string.Empty;
    public Aircraft Aircraft { get; set; } = null!;

    public DateTime DepartureUTC { get; set; }
    public DateTime ArrivalUTC { get; set; }

    public decimal PriceInEur { get; set; }
    public int TakenSeats { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<FlightPassenger> Passengers { get; } = new HashSet<FlightPassenger>();
    public ICollection<BookingFlight> Bookings { get; } = new HashSet<BookingFlight>();

    public int DurationInMinutes
        => (int)(ArrivalUTC - DepartureUTC).TotalMinutes;

    public int AvailableSeats
        => Aircraft?.NumberOfSeats - TakenSeats ?? 0;
}