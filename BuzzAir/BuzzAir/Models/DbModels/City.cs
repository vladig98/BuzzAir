namespace BuzzAir.Models.DbModels
{
    public class City
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }

        public State? State { get; set; }
        public string? StateId { get; set; }

        public required Country Country { get; set; }
        public required string CountryId { get; set; }

        public required Timezone Timezone { get; set; }
        public required string TimezoneId { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Airport> Airports { get; set; } = new HashSet<Airport>();
    }
}
