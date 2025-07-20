namespace BuzzAir.Areas.Admin.ViewModels.StateDTOs
{
    public class EditStateVM
    {
        [Required]
        [HiddenInput]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [HiddenInput]
        public string CountryId { get; set; } = string.Empty;
        [HiddenInput]
        public Country? Country { get; set; }
        public string CountryName { get; set; } = string.Empty;

        public List<SelectListItem> Countries { get; set; } = [];
    }
}
