namespace BuzzAir.Data.Models;

public abstract class Service
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public abstract decimal Price { get; init; }
    public abstract string Name { get; init; }

    public ICollection<PassengerService> Passengers { get; } = new HashSet<PassengerService>();
}
