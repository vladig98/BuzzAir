namespace BuzzAir.Areas.Admin.ViewModels.CityDTOs
{
    public class CreateCityVM
    {
        public string Name { get; set; } = string.Empty;
        [HiddenInput]
        public string CountryId { get; set; } = string.Empty;
        [HiddenInput]
        public Country? Country { get; set; }
        [HiddenInput]
        public string StateId { get; set; } = string.Empty;
        [HiddenInput]
        public State? State { get; set; }
        public List<SelectListItem> CountryOptions { get; set; } = [];
        public List<SelectListItem> StatesOptions { get; set; } = [];
    }
}
