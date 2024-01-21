using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Models.ViewModels
{
    public class CityViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; }
    }
}
