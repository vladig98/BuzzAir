using BuzzAir.Utilities;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.CreateModels
{
    public class AircraftCreateViewModel
    {
        public AircraftCreateViewModel()
        {
        }

        [Required]
        [Display(Prompt = nameof(Name))]
        [RegularExpression("[a-zA-Z0-9-]{3,}")]
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.MinimumNumberOfSeatsForAnAircraft, GlobalConstants.MaximumNumberOfSeatsForAnAircraft)]
        [Display(Prompt = "Number of seats")]
        public int Seats { get; set; }
    }
}
