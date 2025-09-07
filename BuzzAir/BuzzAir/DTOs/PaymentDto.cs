namespace BuzzAir.DTOs;

public class PaymentDto
{
    public string CardHolder { get; set; } = string.Empty;
    public string CardToken { get; set; } = string.Empty;
    public decimal AmountInEur { get; set; }
    public string Currency { get; set; } = "EUR";
}
