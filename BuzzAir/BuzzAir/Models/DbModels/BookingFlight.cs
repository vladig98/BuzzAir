namespace BuzzAir.Models.DbModels
{
    public class BookingFlight
    {
        public required string BookingId { get; set; }
        public required Booking Booking { get; set; }

        public required string FlightId { get; set; }
        public required Flight Flight { get; set; }
    }
}
