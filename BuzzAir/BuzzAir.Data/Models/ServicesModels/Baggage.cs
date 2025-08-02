namespace BuzzAir.Data.Models.ServicesModels;

public class Baggage : Service
{
    public override string Name { get; set; } = nameof(Baggage);
    public BaggageType BaggageType { get; set; }

    public override decimal Price { get; set; }
    public int Kilos { get; private set; }
}