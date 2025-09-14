namespace BuzzAir.DTOs;

public class SeatMapDto
{
    public int Rows { get; set; }
    public int Cols { get; set; } = 6;
    public IList<string> Taken { get; } = [];
    public IList<string> Locked { get; } = [];
    public IList<string>? ExtraLegRoom { get; } = [];
}
