using BuzzAir.Models.DbModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class PassengerViewModel
    {
        public PassengerViewModel()
        {
            Services = new List<ServiceViewModel>();
        }

        [Required]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "The first name must contain only latin letters.")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "The last name must contain only latin letters.")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public BaggageType BaggageType { get; set; }

        public List<ServiceViewModel> Services { get; set; }
    }
}
