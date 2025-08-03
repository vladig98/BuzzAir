namespace BuzzAir.Areas.Admin.ViewModels.CityViewModels;

public sealed class CreateCityVM
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string CountryId { get; set; } = string.Empty;
    public ICollection<SelectListItem> Countries { get; } = [];

    public string? StateId { get; set; }
    public ICollection<SelectListItem> States { get; } = [];

    [Required]
    public string TimezoneId { get; set; } = string.Empty;
    public ICollection<SelectListItem> Timezones { get; } = [];
}
