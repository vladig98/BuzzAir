namespace BuzzAir.Areas.Admin.ViewModels.CityViewModels;

public class RestoreCityVM
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string? StateName { get; set; }
    public string TimezoneName { get; set; } = string.Empty;
}
