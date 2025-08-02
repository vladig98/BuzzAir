namespace BuzzAir.Data.Models;

public class Booking
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string PaymentId { get; set; } = string.Empty;
    public Payment Payment { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<BookingFlight> Flights { get; } = new HashSet<BookingFlight>();
    public ICollection<BookingPassenger> Passengers { get; } = new HashSet<BookingPassenger>();
}
