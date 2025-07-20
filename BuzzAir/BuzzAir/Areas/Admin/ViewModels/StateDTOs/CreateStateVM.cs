namespace BuzzAir.Areas.Admin.ViewModels.StateDTOs
{
    public class CreateStateVM
    {
        public string Name { get; set; } = string.Empty;
        [HiddenInput]
        public string CountryId { get; set; } = string.Empty;
        [HiddenInput]
        public Country? Country { get; set; }
        public List<SelectListItem> CountryOptions { get; set; } = [];
    }
}
