namespace BuzzAir.Models
{
    public class BookingFlight
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
