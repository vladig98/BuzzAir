namespace BuzzAir.Data.Models.ServicesModels;

public class AirportCheckIn : Service
{
    public override decimal Price { get; init; } = Constants.AirportCheckInPrice;
    public override string Name { get; init; } = nameof(AirportCheckIn);
}
