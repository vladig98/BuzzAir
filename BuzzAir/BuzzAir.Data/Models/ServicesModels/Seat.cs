namespace BuzzAir.Data.Models.ServicesModels;

public class Seat : Service
{
    private Seat() { }

    public Seat(SeatType type = SeatType.None)
    {
        SeatType = type;
        Price = type switch
        {
            SeatType.None => Constants.SeatPriceNone,
            SeatType.Normal => Constants.SeatPriceNormal,
            SeatType.ExtraLegRoom => Constants.SeatPriceExtraLegRoom,
            _ => throw new InvalidOperationException("Invalid seat type")
        };
    }

    public override string Name { get; init; } = nameof(Seat);
    public override decimal Price { get; init; }

    public SeatType SeatType { get; init; }
}
