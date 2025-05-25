using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services
{
    public class FlightPassengerService(
        BuzzAirDbContext context, 
        ISeatService seatService) : IFlightPassengerService
    {
        public async Task<FlightPassenger> Create(IPassenger passenger, Flight flight)
        {
            FlightSeat seat = await seatService.AssignSeat(passenger, flight);
            FlightPassenger flightPax = PassengerFactory.CreatePassengerForFlight(flight, passenger, seat);

            await context.FlightPassengers.AddAsync(flightPax);
            await context.SaveChangesAsync();

            return flightPax;
        }

        public async Task CreateAsync(List<IPassenger> passengers, Flight? flight)
        {
            if (flight == null)
            {
                return;
            }

            List<Task> tasks = [];

            foreach (IPassenger passenger in passengers)
            {
                Task passengerTask = Create(passenger, flight);

                tasks.Add(passengerTask);
            }

            await Task.WhenAll(tasks);
        }
    }
}
