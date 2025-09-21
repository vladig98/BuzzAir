namespace BuzzAir.Services;

public class SeatService : ISeatService
{
    public int GetSeatNumberAsync(Seat? seatService, Flight flight, Passenger passenger, int? seatSelection)
    {
        if (flight is null || passenger is null)
        {
            throw new InvalidOperationException("Invalid flight or passenger");
        }

        HashSet<SeatMap> seatMap = [.. flight.Aircraft.SeatMap];
        HashSet<int> takenSeatNumbers = [.. flight.Passengers.Select(x => x.SeatNumber)];

        List<SeatMap> normalSeats = [];
        List<SeatMap> extraLegRoomSeats = [];

        foreach (SeatMap? map in seatMap)
        {
            if (takenSeatNumbers.Contains(map.SeatNumber))
            {
                continue;
            }

            if (map.SeatType == SeatMapType.Normal)
            {
                normalSeats.Add(map);
                continue;
            }

            if (map.SeatType == SeatMapType.ExtraLegRoom)
            {
                extraLegRoomSeats.Add(map);
                continue;
            }
        }

        return seatService is null
            ? PickRandomSeat(normalSeats)
            : seatService.SeatType switch
            {
                SeatType.None => PickRandomSeat(normalSeats),
                SeatType.Normal => CheckIfSeatIsAvailable(normalSeats, seatSelection),
                SeatType.ExtraLegRoom => CheckIfSeatIsAvailable(extraLegRoomSeats, seatSelection),
                _ => throw new InvalidOperationException("Invalid seat selection"),
            };
    }

    private static int CheckIfSeatIsAvailable(List<SeatMap> seatMap, int? seatSelection)
    {
        if (!seatSelection.HasValue)
        {
            throw new InvalidOperationException("Invalid seat selection");
        }

        SeatMap map = seatMap.FirstOrDefault(x => x.SeatNumber == seatSelection)
            ?? throw new InvalidOperationException("Invalid seat selection");

        return map.SeatNumber;
    }

    private static int PickRandomSeat(List<SeatMap> availableSeatNumber)
    {
        int seatIndex = RandomNumberGenerator.GetInt32(0, availableSeatNumber.Count);
        int seatNumber = availableSeatNumber[seatIndex].SeatNumber;

        return seatNumber;
    }
}
