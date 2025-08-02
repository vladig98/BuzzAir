namespace BuzzAir.Data.Models;

public class State
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public Country Country { get; set; } = null!;
    public string CountryId { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public virtual ICollection<City> Cities { get; } = new HashSet<City>();
}
