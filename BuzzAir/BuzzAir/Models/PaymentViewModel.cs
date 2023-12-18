using BuzzAir.Models.DbModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class PaymentViewModel
    {
        [Required]
        [RegularExpression("\\d{16,16}", ErrorMessage = "The card number is invalid")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Prompt = "MM / yy")]
        public string ExpiryDate { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z ]+", ErrorMessage = "The card holder must contain only latin letters.")]
        public string CardHolder { get; set; }

        [Required]
        [RegularExpression("\\d{3,3}", ErrorMessage = "The CVC number is invalid")]
        public string CVC { get; set; }

        [Required]
        public CardType CardType { get; set; }

        [Required]
        public Currency Currency { get; set; }
    }
}
