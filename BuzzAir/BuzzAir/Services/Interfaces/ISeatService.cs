namespace BuzzAir.Services.Interfaces;

public interface ISeatService
{
    int GetSeatNumberAsync(Seat? seatService, Flight flight, Passenger passenger, int? seatSelection);
}
