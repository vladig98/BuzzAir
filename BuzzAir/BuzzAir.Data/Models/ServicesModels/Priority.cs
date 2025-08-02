namespace BuzzAir.Data.Models.ServicesModels;

public class Priority : Service
{
    public override decimal Price { get; set; } = Constants.PriorityPrice;
    public override string Name { get; set; } = nameof(Priority);
}
