namespace BuzzAir.Models.DbModels
{
    public class UserBooking
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
        [Required]
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();
        [Required]
        public string BookingId { get; set; } = string.Empty;
        [Required]
        public Booking Booking { get; set; } = new Booking();
    }
}
