namespace BuzzAir.Data.Models;

public class PassengerService
{
    public string PassengerId { get; set; } = string.Empty;
    public Passenger Passenger { get; set; } = null!;

    public string ServiceId { get; set; } = string.Empty;
    public Service Service { get; set; } = null!;
}
