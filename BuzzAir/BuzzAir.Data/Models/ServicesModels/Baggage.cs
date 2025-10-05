namespace BuzzAir.Data.Models.ServicesModels;

public class Baggage : Service
{
    private Baggage() { }

    public Baggage(BaggageType type)
    {
        BaggageType = type;
        Price = type switch
        {
            BaggageType.Cabin => Constants.PriceForCabin,
            BaggageType.TwentyKilos => Constants.PriceFor20kg,
            BaggageType.ThirtyTwoKilos => Constants.PriceFor32kg,
            _ => throw new InvalidOperationException("Invalid baggage type")
        };
        Kilos = type switch
        {
            BaggageType.Cabin => Constants.CabinKilos,
            BaggageType.TwentyKilos => Constants.TwentyKilos,
            BaggageType.ThirtyTwoKilos => Constants.ThirtyTwoKilos,
            _ => throw new InvalidOperationException("Invalid baggage type")
        };
    }

    public override string Name { get; init; } = nameof(Baggage);
    public override decimal Price { get; init; }

    public BaggageType BaggageType { get; private set; }
    public int Kilos { get; private set; }
}