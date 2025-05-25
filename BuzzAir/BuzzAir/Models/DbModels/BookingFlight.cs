namespace BuzzAir.Models.DbModels
{
    public class BookingFlight
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string BookingId { get; set; } = string.Empty;
        [Required]
        public Booking Booking { get; set; } = new Booking();
        [Required]
        public string FlightId { get; set; } = string.Empty;
        [Required]
        public Flight Flight { get; set; } = new Flight();
        public bool IsOutbound { get; set; }
    }
}
