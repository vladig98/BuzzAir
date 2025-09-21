namespace BuzzAir.Data.Models;
public sealed class SeatMap
{
    public string AircraftId { get; set; } = string.Empty;
    public Aircraft Aircraft { get; set; } = null!;

    public int SeatNumber { get; set; }
    public SeatMapType SeatType { get; set; } = SeatMapType.Normal;
}
