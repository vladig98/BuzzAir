
namespace BuzzAir.Factories
{
    public static class PaymentFactory
    {
        internal static Payment Create(PaymentViewModel viewModel, DateTime expiryDate, decimal bookingPrice)
        {
            Payment payment = new()
            {
                Amount = bookingPrice,
                ExpiryDate = expiryDate,
                Card = viewModel.CardType,
                CardHolder = viewModel.CardHolder,
                CardNumber = viewModel.CardNumber,
                Currency = viewModel.Currency,
                CVC = viewModel.CVC
            };

            return payment;
        }
    }
}
