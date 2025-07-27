namespace BuzzAir.Models.DbModels
{
    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string PaymentId { get; set; }
        public required Payment Payment { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<BookingFlight> Flights { get; set; } = new HashSet<BookingFlight>();
        public ICollection<BookingPassenger> Passengers { get; set; } = new HashSet<BookingPassenger>();
    }
}
