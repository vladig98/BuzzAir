namespace BuzzAir.Data.Models;

public class Aircraft
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int NumberOfSeats { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public ICollection<Flight> Flights { get; } = new HashSet<Flight>();
    public ICollection<SeatMap> SeatMap { get; } = new HashSet<SeatMap>();
}