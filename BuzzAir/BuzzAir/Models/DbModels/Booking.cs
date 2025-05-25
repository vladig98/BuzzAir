namespace BuzzAir.Models.DbModels
{
    public class Booking
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public ICollection<BookingFlight> Flights { get; set; } = [];
        [Required]
        public ICollection<BookingPassenger> Passengers { get; set; } = [];
        [Required]
        public string PaymentId { get; set; } = string.Empty;
        public Payment Payment { get; set; } = new Payment();
        public bool IsDeleted { get; set; }
    }
}
