using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Enums;
using BuzzAir.Services.Contracts;

namespace BuzzAir.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BuzzAirDbContext _context;

        public PaymentService(BuzzAirDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> Create(CardType cardtype, string cardNumber, DateTime expiryDate, string cardHolder, string cvc, decimal amount, Currency currency)
        {
            Payment payment = new Payment
            {
                Card = cardtype,
                CardNumber = cardNumber,
                ExpiryDate = expiryDate,
                CardHolder = cardHolder,
                CVC = cvc,
                Amount = amount,
                Currency = currency,
                Id = Guid.NewGuid().ToString()
            };

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment;
        }
    }
}
