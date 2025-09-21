namespace BuzzAir.Services.Interfaces;

public interface IPaymentService
{
    Task<Payment> AddPaymentAsync(PaymentDto data, decimal totalAmountInEur, CancellationToken token);
}
