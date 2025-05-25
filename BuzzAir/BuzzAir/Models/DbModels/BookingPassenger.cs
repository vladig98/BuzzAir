namespace BuzzAir.Models.DbModels
{
    public class BookingPassenger
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string BookingId { get; set; } = string.Empty;
        [Required]
        public Booking Booking { get; set; } = new Booking();
        [Required]
        public string PassengerId { get; set; } = string.Empty;
        [Required]
        public Passenger Passenger { get; set; } = new Passenger();
    }
}
