namespace BuzzAir.Data.Models.ServicesModels;

public class Flexibility : Service
{
    public override decimal Price { get; set; } = Constants.FlexibilityPrice;
    public override string Name { get; set; } = nameof(Flexibility);
}
