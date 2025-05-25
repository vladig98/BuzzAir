namespace BuzzAir.Models.DbModels
{
    public class FlightSeat
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string FlightId { get; set; } = string.Empty;
        public Flight Flight { get; set; } = new Flight();
        [Required]
        public int SeatNumber { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
    }
}
