namespace BuzzAir.Models.DbModels
{
    public class Aircraft
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int NumberOfSeats { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }
}