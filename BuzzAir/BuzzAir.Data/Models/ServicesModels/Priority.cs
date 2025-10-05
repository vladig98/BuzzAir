namespace BuzzAir.Data.Models.ServicesModels;

public class Priority : Service
{
    public override decimal Price { get; init; } = Constants.PriorityPrice;
    public override string Name { get; init; } = nameof(Priority);
}
