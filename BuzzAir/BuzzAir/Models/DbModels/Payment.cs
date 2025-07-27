namespace BuzzAir.Models.DbModels
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public CardType Card { get; set; }
        public DateTime ExpiryDate { get; set; }

        public required string CardNumber { get; set; }
        public required string CardHolder { get; set; }

        public required string BookingId { get; set; }
        public required Booking Booking { get; set; }

        public required string CVC { get; set; }
        public decimal AmountInEur { get; set; }
    }
}