namespace BuzzAir.Services.Contracts
{
    public interface IPriceCalculator
    {
        decimal Calculate(CreateBookingViewModel model, Flight outbound, Flight? inbound);
    }
}
