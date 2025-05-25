namespace BuzzAir.Models.DbModels
{
    public class Payment
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public CardType Card { get; set; }
        [Required]
        public string CardNumber { get; set; } = string.Empty;
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string CardHolder { get; set; } = string.Empty;
        [Required]
        public string CVC { get; set; } = string.Empty;
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}