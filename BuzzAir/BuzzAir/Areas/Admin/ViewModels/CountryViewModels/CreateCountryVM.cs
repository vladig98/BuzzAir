namespace BuzzAir.Areas.Admin.ViewModels.CountryViewModels;

public sealed class CreateCountryVM
{
    public string Name { get; set; } = string.Empty;
    public string ISO { get; set; } = string.Empty;
    public bool IsOfficiallyRecognizedCountry { get; set; }
}
