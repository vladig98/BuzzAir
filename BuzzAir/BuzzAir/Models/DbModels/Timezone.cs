namespace BuzzAir.Models.DbModels
{
    public class Timezone
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }
        public TimeSpan Offset { get; set; }

        public required string Identifier { get; set; }
        public required string Abbreviation { get; set; }

        public bool UsesDST { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();
    }
}
