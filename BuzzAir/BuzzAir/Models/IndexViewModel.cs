using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Origins = new List<SelectListItem>();
            Passengers = 1;
        }

        public IEnumerable<SelectListItem> Origins { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }

        [DataType(DataType.Date)]
        public DateTime Departure { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Return { get; set; }

        [Range(1, 10)]
        public int Passengers { get; set; }

        public string isReturning { get; set; }
    }
}
