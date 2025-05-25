namespace BuzzAir.Services.Interfaces
{
    public interface IFlightPassengerService
    {
        Task<FlightPassenger> Create(IPassenger passenger, Flight flight);
        Task CreateAsync(List<IPassenger> passengers, Flight? outbound);
    }
}
