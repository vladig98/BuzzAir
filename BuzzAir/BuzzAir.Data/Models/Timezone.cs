namespace BuzzAir.Data.Models;

public class Timezone
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;
    public TimeSpan Offset { get; set; }

    public string Identifier { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;

    public bool UsesDST { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<City> Cities { get; } = new HashSet<City>();
}
