namespace BuzzAir.Hubs;

public class SeatMapHub(IFlightService flightService) : Hub
{
    public async Task SendSeatMap(string flightId, string direction)
    {
        Flight? flight = await flightService.GetFlightModelByIdAsync(flightId, Context.ConnectionAborted);

        if (flight is null)
        {
            return;
        }

        List<SeatMap> seatMap = [.. flight.Aircraft.SeatMap];
        HashSet<int> takenSeats = [.. flight.Passengers.Select(x => x.SeatNumber)];

        List<SeatDTO> seats = [];

        foreach (SeatMap map in seatMap)
        {
            SeatType type = map.SeatType == SeatMapType.Normal ? SeatType.Normal : SeatType.ExtraLegRoom;
            bool isTaken = takenSeats.Contains(map.SeatNumber);

            SeatDTO dto = new(map.SeatNumber, type.ToString(), isTaken);
            seats.Add(dto);
        }

        await Clients.Caller.SendAsync("ReceiveSeatMap", seats, direction, Context.ConnectionAborted);
    }
}
