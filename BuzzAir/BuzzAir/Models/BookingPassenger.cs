namespace BuzzAir.Models
{
    public class BookingPassenger
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        public int PersonId { get; set; }
        public virtual IPerson Person { get; set; }
    }
}
