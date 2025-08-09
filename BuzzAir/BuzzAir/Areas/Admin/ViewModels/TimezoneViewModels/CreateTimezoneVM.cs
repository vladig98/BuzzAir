namespace BuzzAir.Areas.Admin.ViewModels.TimezoneViewModels;

public class CreateTimezoneVM
{
    public string Name { get; set; } = string.Empty;

    public string Identifier { get; set; } = string.Empty;

    public string Abbreviation { get; set; } = string.Empty;

    public TimeSpan Offset { get; set; }

    public bool UsesDST { get; set; }
}
