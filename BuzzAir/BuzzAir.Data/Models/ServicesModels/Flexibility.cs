namespace BuzzAir.Data.Models.ServicesModels;

public class Flexibility : Service
{
    public override decimal Price { get; init; } = Constants.FlexibilityPrice;
    public override string Name { get; init; } = nameof(Flexibility);
}
