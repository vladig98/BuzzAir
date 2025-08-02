namespace BuzzAir.Data.Models.ServicesModels;

public class AirportCheckIn : Service
{
    public override decimal Price { get; set; } = Constants.AirportCheckInPrice;
    public override string Name { get; set; } = nameof(AirportCheckIn);
}
