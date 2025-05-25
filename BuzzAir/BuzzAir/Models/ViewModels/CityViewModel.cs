namespace BuzzAir.Models.ViewModels
{
    public class CityViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; } = string.Empty;
    }
}
