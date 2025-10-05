namespace BuzzAir.Data.Models.ServicesModels;

public class OnTimeArrival : Service
{
    public override decimal Price { get; init; } = Constants.OnTimeArrivalPrice;
    public override string Name { get; init; } = nameof(OnTimeArrival);
}
