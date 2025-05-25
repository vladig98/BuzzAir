namespace BuzzAir.Models.DbModels
{
    public class State
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Country Country { get; set; } = new Country();
        public string CountryId { get; set; } = string.Empty;
        public ICollection<Address> Addresses { get; set; } = [];
        public ICollection<Airport> Airports { get; set; } = [];
        public ICollection<City> Cities { get; set; } = [];
    }
}
