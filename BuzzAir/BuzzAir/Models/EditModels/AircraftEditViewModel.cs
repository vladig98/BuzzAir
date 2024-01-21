using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Models.EditModels
{
    public class AircraftEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int NumbeOfSeats { get; set; }
    }
}
