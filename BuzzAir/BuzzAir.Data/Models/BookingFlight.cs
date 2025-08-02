namespace BuzzAir.Data.Models;

public class BookingFlight
{
    public string BookingId { get; set; } = string.Empty;
    public Booking Booking { get; set; } = null!;

    public string FlightId { get; set; } = string.Empty;
    public Flight Flight { get; set; } = null!;
}
