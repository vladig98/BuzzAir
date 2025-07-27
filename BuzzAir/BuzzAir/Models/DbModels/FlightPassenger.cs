namespace BuzzAir.Models.DbModels
{
    public class FlightPassenger
    {
        public required Flight Flight { get; set; }
        public required string FlightId { get; set; }

        public required Passenger Passenger { get; set; }
        public required string PassengerId { get; set; }

        public int SeatNumber { get; set; }
    }
}
