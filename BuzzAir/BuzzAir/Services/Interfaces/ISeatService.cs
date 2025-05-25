namespace BuzzAir.Services.Interfaces
{
    public interface ISeatService
    {
        Task<FlightSeat> AssignSeat(IPassenger passenger, Flight flight);
        Task CreateSeats(Flight flight, int numberOfSeats);
    }
}
