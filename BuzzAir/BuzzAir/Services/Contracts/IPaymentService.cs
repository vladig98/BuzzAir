namespace BuzzAir.Services.Contracts
{
    public interface IPaymentService
    {
        Task<Payment> CreateAsync(PaymentViewModel payment, DateTime expiryDate, decimal bookingPrice);
    }
}
