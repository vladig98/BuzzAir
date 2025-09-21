namespace BuzzAir.Services.Interfaces;

public interface IPassengersService
{
    Task<Passenger> CreatePassengerAsync(PassengerDto data, CancellationToken token);
}
