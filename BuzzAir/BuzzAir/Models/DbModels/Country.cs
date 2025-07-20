namespace BuzzAir.Models.DbModels
{
    public class Country
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string ISO { get; set; } = string.Empty;
        // true for officially recognized countries and false for dependencies
        [Required]
        public bool IsCountry { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<TravelDocument> Nationalities { get; set; } = new List<TravelDocument>();
        public IEnumerable<TravelDocument> BirthCountries { get; set; } = new List<TravelDocument>();
        public IEnumerable<City> Cities { get; set; } = new List<City>();
    }
}
