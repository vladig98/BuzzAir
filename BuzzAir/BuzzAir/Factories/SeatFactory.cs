namespace BuzzAir.Factories
{
    public static class SeatFactory
    {
        internal static FlightSeat Create(Flight flight, int seatNumber)
        {
            FlightSeat seat = new()
            {
                Flight = flight,
                FlightId = flight.Id,
                SeatNumber = seatNumber
            };

            return seat;
        }
    }
}
