namespace BuzzAir.Areas.Admin.ViewModels.CountryDTOs
{
    public class CreateCountryVM
    {
        public string Name { get; set; } = string.Empty;
        public string ISO { get; set; } = string.Empty;
        public bool IsCountry { get; set; }
    }
}
