namespace BuzzAir.Data.Models;

public class City
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public State? State { get; set; }
    public string? StateId { get; set; }

    public Country Country { get; set; } = null!;
    public string CountryId { get; set; } = string.Empty;

    public Timezone Timezone { get; set; } = null!;
    public string TimezoneId { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public ICollection<Airport> Airports { get; } = new HashSet<Airport>();
    public ICollection<ApplicationUser> Users { get; } = new HashSet<ApplicationUser>();
}
