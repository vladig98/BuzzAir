using BuzzAir.Models.DbModels.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Models.ViewModels
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
            Kilos = 0;
            Seat = SeatType.Normal.ToString();
        }

        [HiddenInput(DisplayValue = false)]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string URL { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Kilos { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Seat { get; set; }
    }
}
