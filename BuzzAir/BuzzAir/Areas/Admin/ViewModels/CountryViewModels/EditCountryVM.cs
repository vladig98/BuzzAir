namespace BuzzAir.Areas.Admin.ViewModels.CountryViewModels;

public class EditCountryVM
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ISO { get; set; } = string.Empty;
    public bool IsOfficiallyRecognizedCountry { get; set; }
}
