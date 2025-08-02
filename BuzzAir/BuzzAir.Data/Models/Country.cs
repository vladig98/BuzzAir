namespace BuzzAir.Data.Models;

public class Country
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string ISO { get; set; } = string.Empty;

    // true for officially recognized countries and false for dependencies
    public bool IsOfficiallyRecognizedCountry { get; set; } = true;
    public bool IsDeleted { get; set; }

    public virtual ICollection<City> Cities { get; } = new HashSet<City>();
    public virtual ICollection<State> States { get; } = new HashSet<State>();
    public virtual ICollection<TravelDocument> DocumentsNationalities { get; } = new HashSet<TravelDocument>();
    public virtual ICollection<TravelDocument> DocumentsBirthCountries { get; } = new HashSet<TravelDocument>();
}
