namespace BuzzAir.Areas.Admin.ViewModels.CityViewModels;

public class EditCityVM
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string CountryId { get; set; } = string.Empty;
    public ICollection<SelectListItem> Countries { get; } = [];

    public string? StateId { get; set; }
    public ICollection<SelectListItem> States { get; } = [];

    public string TimezoneId { get; set; } = string.Empty;
    public ICollection<SelectListItem> Timezones { get; } = [];
}
