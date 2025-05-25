namespace BuzzAir.Models.EditModels
{
    public class AircraftEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int NumbeOfSeats { get; set; }
    }
}
