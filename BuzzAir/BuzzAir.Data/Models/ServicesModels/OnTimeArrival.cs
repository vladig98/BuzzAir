namespace BuzzAir.Data.Models.ServicesModels;

public class OnTimeArrival : Service
{
    public override decimal Price { get; set; } = Constants.OnTimeArrivalPrice;
    public override string Name { get; set; } = nameof(OnTimeArrival);
}
