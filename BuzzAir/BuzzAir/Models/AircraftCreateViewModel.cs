using BuzzAir.Utilities;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class AircraftCreateViewModel
    {
        public AircraftCreateViewModel()
        {
        }

        [Required]
        [Display(Prompt = nameof(Name))]
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.MinimumNumberOfSeatsForAnAircraft, GlobalConstants.MaximumNumberOfSeatsForAnAircraft)]
        [Display(Prompt = "Number of seats")]
        public int Seats { get; set; }
    }
}
