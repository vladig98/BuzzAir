namespace BuzzAir.Data.Models;

public class BookingPassenger
{
    public string BookingId { get; set; } = string.Empty;
    public Booking Booking { get; set; } = null!;

    public string PassengerId { get; set; } = string.Empty;
    public Passenger Passenger { get; set; } = null!;
}
