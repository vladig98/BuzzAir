namespace BuzzAir.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreateAsync(PaymentViewModel payment, DateTime expiryDate, decimal bookingPrice);
    }
}
