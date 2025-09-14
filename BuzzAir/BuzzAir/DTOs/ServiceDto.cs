namespace BuzzAir.DTOs;

public class ServiceDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public BaggageType? BaggageType { get; set; }
    public int? Kilos { get; set; }
    public SeatType? SeatType { get; set; }
}
