namespace BuzzAir.Data.Models;

public class Payment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public CardType Card { get; set; }
    public DateTime ExpiryDate { get; set; }

    public string CardNumber { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;

    public string BookingId { get; set; } = string.Empty;
    public Booking Booking { get; set; } = null!;

    public string CVC { get; set; } = string.Empty;
    public decimal AmountInEur { get; set; }
}