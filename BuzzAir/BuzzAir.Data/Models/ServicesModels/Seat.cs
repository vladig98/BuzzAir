namespace BuzzAir.Data.Models.ServicesModels;

public class Seat : Service
{
    public override decimal Price { get; set; } = Constants.SeatPrice;
    public SeatType SeatType { get; set; } = SeatType.Normal;
    public override string Name { get; set; } = nameof(Seat);
}
