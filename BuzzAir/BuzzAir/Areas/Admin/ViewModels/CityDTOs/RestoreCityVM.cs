namespace BuzzAir.Areas.Admin.ViewModels.CityDTOs
{
    public class RestoreCityVM
    {
        [Required]
        [HiddenInput]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
    }
}
