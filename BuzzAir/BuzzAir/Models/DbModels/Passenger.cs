namespace BuzzAir.Models.DbModels
{
    public class Passenger
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public required string DocumentId { get; set; }
        public required TravelDocument Document { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<PassengerService> Services { get; set; } = new HashSet<PassengerService>();
        public ICollection<FlightPassenger> Flights { get; set; } = new HashSet<FlightPassenger>();
        public ICollection<BookingPassenger> Bookings { get; set; } = new HashSet<BookingPassenger>();
    }
}