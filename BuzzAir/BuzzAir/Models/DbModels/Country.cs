namespace BuzzAir.Models.DbModels
{
    public class Country
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string ISO { get; set; }

        // true for officially recognized countries and false for dependencies
        public bool IsOfficiallyRecognizedCountry { get; set; } = true;
        public bool IsDeleted { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();
        public ICollection<State> States { get; set; } = new HashSet<State>();
        public ICollection<TravelDocument> DocumentsNationalities { get; set; } = new HashSet<TravelDocument>();
        public ICollection<TravelDocument> DocumentsBirthCountries { get; set; } = new HashSet<TravelDocument>();
    }
}
