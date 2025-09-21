namespace BuzzAir.Services;

public class PaymentService(BuzzAirDbContext dbContext) : IPaymentService
{
    public async Task<Payment> AddPaymentAsync(PaymentDto data, decimal totalAmountInEur, CancellationToken token)
    {
        if (data is null)
        {
            throw new InvalidOperationException("Invalid payment data");
        }

        if (totalAmountInEur != data.AmountInEur)
        {
            throw new InvalidOperationException("Payment amount differs");
        }

        Payment payment = new()
        {
            Card = data.CardType,
            CardHolder = data.CardHolder,
            CardNumber = data.CardNumber,
            CVC = data.CVC,
            ExpiryDate = DateTime.SpecifyKind(data.ExpiryDate, DateTimeKind.Utc),
            AmountInEur = totalAmountInEur
        };

        _ = await dbContext.Payments.AddAsync(payment, token);

        return payment;
    }
}
