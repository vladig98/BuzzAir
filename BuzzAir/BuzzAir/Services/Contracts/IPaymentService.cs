using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Enums;

namespace BuzzAir.Services.Contracts
{
    public interface IPaymentService
    {
        Task<Payment> Create(CardType cardtype, string cardNumber, DateTime expiryDate, string cardHolder, string cvc, decimal amount, Currency currency);
    }
}
