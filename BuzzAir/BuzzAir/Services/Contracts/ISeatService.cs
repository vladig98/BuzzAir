using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services.Contracts
{
    public interface ISeatService
    {
        Task<FlightSeat> AssignSeat(IPassenger passenger, Flight flight);
    }
}
