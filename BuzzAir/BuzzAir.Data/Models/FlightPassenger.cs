namespace BuzzAir.Data.Models;

public class FlightPassenger
{
    public Flight Flight { get; set; } = null!;
    public string FlightId { get; set; } = string.Empty;

    public Passenger Passenger { get; set; } = null!;
    public string PassengerId { get; set; } = string.Empty;

    public int SeatNumber { get; set; }
}
