namespace BuzzAir.Models
{
    public class FlightPassenger
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
