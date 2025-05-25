namespace BuzzAir.Services
{
    public class PaymentService(BuzzAirDbContext context) : IPaymentService
    {
        public async Task<Payment> CreateAsync(PaymentViewModel viewModel, DateTime expiryDate, decimal bookingPrice)
        {
            Payment payment = PaymentFactory.Create(viewModel, expiryDate, bookingPrice);

            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync();

            return payment;
        }
    }
}
