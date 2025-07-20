namespace BuzzAir.Areas.Admin.ViewModels.CityDTOs
{
    public class EditCityVM
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
        [HiddenInput]
        public string? StateId { get; set; } = string.Empty;
        [HiddenInput]
        public State? State { get; set; }
        public string? StateName { get; set; } = string.Empty;

        public List<SelectListItem> Countries { get; set; } = [];
        public List<SelectListItem> States { get; set; } = [];
    }
}
