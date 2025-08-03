namespace BuzzAir.Areas.Admin.ViewModels.StateViewModels;

public sealed class EditStateVM
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string CountryId { get; set; } = string.Empty;
    public ICollection<SelectListItem> Countries { get; } = [];
}
