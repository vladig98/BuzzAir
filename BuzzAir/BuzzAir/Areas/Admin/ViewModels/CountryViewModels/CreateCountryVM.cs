namespace BuzzAir.Areas.Admin.ViewModels.CountryViewModels;

public sealed class CreateCountryVM
{
    public string Name { get; set; } = string.Empty;
    public string ISO2 { get; set; } = string.Empty;
    public string ISO3 { get; set; } = string.Empty;
    public bool IsOfficiallyRecognizedCountry { get; set; }
}
