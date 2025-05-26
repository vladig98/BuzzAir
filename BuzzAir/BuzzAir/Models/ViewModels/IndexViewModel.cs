namespace BuzzAir.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<SelectListItem> Origins { get; set; } = [];
        public IEnumerable<SelectListItem> Destinations { get; set; } = [];
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime Departure { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Return { get; set; }
        [Range(1, 10)]
        public int Passengers { get; set; }
        public string IsReturning { get; set; } = string.Empty;
    }
}
