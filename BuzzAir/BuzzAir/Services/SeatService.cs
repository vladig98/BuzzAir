using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services
{
    public class SeatService(BuzzAirDbContext context) : ISeatService
    {
        private readonly Random _random = Random.Shared;

        public async Task<FlightSeat> AssignSeat(IPassenger passenger, Flight flight)
        {
            FlightSeat[] availableSeats = await context.FlightSeats.Where(x => x.FlightId == flight.Id).Where(x => x.IsAvailable).ToArrayAsync();
            int seatNumber = _random.Next(0, availableSeats.Length);

            FlightSeat seat = flight.Seats.First(x => x.SeatNumber == seatNumber);
            seat.IsAvailable = false;

            await context.SaveChangesAsync();

            return seat;
        }
    }
}
