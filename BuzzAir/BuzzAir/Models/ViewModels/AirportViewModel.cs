using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Models.ViewModels
{
    public class AirportViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string IATA { get; set; }

        [HiddenInput(DisplayValue = false)]
        public CityViewModel City { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; }
    }
}
