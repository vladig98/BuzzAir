namespace BuzzAir.DTOs;

public class PaymentDto
{
    public CardType CardType { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string CardHolder { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public decimal AmountInEur { get; set; }
    public string CVC { get; set; } = string.Empty;
}
