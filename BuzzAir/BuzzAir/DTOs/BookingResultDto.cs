namespace BuzzAir.DTOs;

public class BookingResultDto
{
    public string BookingId { get; set; } = string.Empty;
    public Uri? RedirectToPaymentUrl { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
