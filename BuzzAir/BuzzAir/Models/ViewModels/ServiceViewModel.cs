namespace BuzzAir.Models.ViewModels
{
    public class ServiceViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public decimal Price { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string URL { get; set; } = string.Empty;
        [HiddenInput(DisplayValue = false)]
        public int Kilos { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Seat { get; set; } = SeatType.Normal.ToString();
    }
}
