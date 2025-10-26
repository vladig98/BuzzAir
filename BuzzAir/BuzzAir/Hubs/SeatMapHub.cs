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

        for (int i = 0; i < seatMap.Count; i += 6)
        {
            // Left 3 seats
            for (int j = i; j < i + 3 && j < seatMap.Count; j++)
            {
                SeatMap map = seatMap[j];
                SeatType type = map.SeatType == SeatMapType.Normal ? SeatType.Normal : SeatType.ExtraLegRoom;
                bool isTaken = takenSeats.Contains(map.SeatNumber);

                SeatDTO dto = new(map.SeatNumber, type.ToString(), isTaken, false);
                seats.Add(dto);
            }

            // Gap
            SeatDTO empty = new(0, string.Empty, false, true);
            seats.Add(empty);

            // Right 3 seats
            for (int j = i + 3; j < i + 6 && j < seatMap.Count; j++)
            {
                SeatMap map = seatMap[j];
                SeatType type = map.SeatType == SeatMapType.Normal ? SeatType.Normal : SeatType.ExtraLegRoom;
                bool isTaken = takenSeats.Contains(map.SeatNumber);

                SeatDTO dto = new(map.SeatNumber, type.ToString(), isTaken, false);
                seats.Add(dto);
            }
        }

        await Clients.Caller.SendAsync("ReceiveSeatMap", seats, direction, Context.ConnectionAborted);
    }
}
