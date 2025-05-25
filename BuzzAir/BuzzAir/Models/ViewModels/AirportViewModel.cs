namespace BuzzAir.Models.ViewModels
{
    public class AirportViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string IATA { get; set; } = string.Empty;

        [HiddenInput(DisplayValue = false)]
        public CityViewModel City { get; set; } = new CityViewModel();

        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; } = string.Empty;
    }
}
