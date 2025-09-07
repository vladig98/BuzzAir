namespace BuzzAir.ViewModels;

public class PaymentViewModel
{
    public CardType Card { get; set; }
    public DateTime ExpiryDate { get; set; }

    public string CardNumber { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;

    public string CVC { get; set; } = string.Empty;
    public decimal AmountInEur { get; set; }
}
