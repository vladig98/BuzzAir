namespace BuzzAir.Data.Models;

public class Passenger
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    public string DocumentId { get; set; } = string.Empty;
    public TravelDocument Document { get; set; } = null!;

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public ICollection<PassengerService> Services { get; } = new HashSet<PassengerService>();
    public ICollection<FlightPassenger> Flights { get; } = new HashSet<FlightPassenger>();
    public ICollection<BookingPassenger> Bookings { get; } = new HashSet<BookingPassenger>();
}