namespace BuzzAir.Services.Interfaces
{
    public interface IPriceCalculator
    {
        decimal Calculate(CreateBookingViewModel model, Flight outbound, Flight? inbound);
    }
}
