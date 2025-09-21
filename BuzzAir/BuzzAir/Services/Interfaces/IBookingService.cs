namespace BuzzAir.Services.Interfaces;

public interface IBookingService
{
    Task CreateBookingAsync(CreateBookingDto data, CancellationToken token);
}
