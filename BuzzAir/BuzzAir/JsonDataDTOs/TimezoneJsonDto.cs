namespace BuzzAir.JsonDataDTOs;

public class TimezoneJsonDto
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public int Offset { get; set; }
    public bool UsesDST { get; set; }
    public ICollection<string> Countries { get; } = [];
}
