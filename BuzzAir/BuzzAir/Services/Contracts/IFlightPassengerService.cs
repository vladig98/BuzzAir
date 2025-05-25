using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services.Contracts
{
    public interface IFlightPassengerService
    {
        Task<FlightPassenger> Create(IPassenger passenger, Flight flight);
        Task CreateAsync(List<IPassenger> passengers, Flight? outbound);
    }
}
